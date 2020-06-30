using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using ERP.Authorization.Users;
using ERP.Issue.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using ERP.Dto;
using ERP.Issue.Exporting;
using ERP.TreeView.Dto;

namespace ERP.Issue
{
    public class IssueAppService : ERPAppServiceBase, IIssueAppService
    {
        private readonly IRepository<Models.Issue, long> _issueRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IssueListExcelExporter _issueListExcelExport;
        private readonly IRepository<Models.Project, long> _projectRepository;

        public IssueAppService(IRepository<Models.Issue, long> issueRepository,
            IRepository<User, long> userRepository,
            IssueListExcelExporter issueListExcelExport,
             IRepository<Models.Project, long> projectRepository
            )
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
            _issueListExcelExport = issueListExcelExport;
            _projectRepository = projectRepository;
        }
        public async Task<long> Create(CreateIssueDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var issue = ObjectMapper.Map<Models.Issue>(input);
            await _issueRepository.InsertAsync(issue);
            await CurrentUnitOfWork.SaveChangesAsync();
            return issue.Id;
        }

        public async Task Delete(long id)
        {
            await _issueRepository.DeleteAsync(id);
        }

        public async Task<FileDto> GetIssueForExcel(GetTreeInputDto input)
        {
            var list = _issueRepository.GetAll()
              .Where(x => x.Type == 2)
              .WhereIf(input.ListStatusId != null, x => input.ListStatusId.Any(a => a == x.Status_Id))
              .WhereIf(input.ListTypeId != null, x => input.ListTypeId.Any(a => a == x.Type_ID))
              .WhereIf(input.ListProjectId != null, x => input.ListProjectId.Any(a => a == x.Parent_Id))
              .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
            x => x.TaskCode.ToUpper().Contains(input.Filter.ToUpper())
            || x.Summary.ToUpper().Contains(input.Filter.ToUpper())
            || x.Status_Id.ToString().Contains(input.Filter)
            || x.CreationTime.ToString().Contains(input.Filter)
            || x.Due_Date.ToString().Contains(input.Filter)
            || x.Estimate.ToString().Contains(input.Filter)
            ).ToList();
            var result = ObjectMapper.Map<List<IssueListDto>>(list);

            return _issueListExcelExport.ExportIssueToFile(result);
        }

        public async Task<FileDto> GetSprintForExcel(IssueInputDto input)
        {
            var list = await GetAll(input);
            var dto = list.Items.ToList();
            return _issueListExcelExport.ExportSprintToFile(dto);
        }

        public async Task<PagedResultDto<IssueListDto>> GetAll(IssueInputDto input)
        {
            var listTask = (from t in _issueRepository.GetAll().Where(x => x.Type == input.Type)
                            join p in _projectRepository.GetAll()
                            on t.Parent_Id equals p.Id
                            select new IssueListDto
                            {
                                Id = t.Id,
                                Parent_Id = t.Parent_Id,
                                Assignee_Id = t.Assignee_Id,
                                CreationTime = t.CreationTime,
                                Discription = t.Discription,
                                Due_Date = t.Due_Date,
                                Estimate = t.Estimate,
                                Priority_Id = t.Priority_Id,
                                ProjectCode = p.ProjectCode,
                                Project_Id = p.Id,
                                Reporter_Id = t.Reporter_Id,
                                Status_Id = t.Status_Id,
                                Summary = t.Summary,
                                TaskCode = t.TaskCode,
                                TenantId = t.TenantId,
                                Type_Id = t.Type_ID,
                                Type = t.Type
                            })
                .WhereIf(input.ListStatusId != null, x => input.ListStatusId.Any(a => a == x.Status_Id))
                .WhereIf(input.ListTypeId != null, x => input.ListTypeId.Any(a => a == x.Type_Id))
                .WhereIf(input.ListProjectId != null, x => input.ListProjectId.Any(a => a == x.Parent_Id))
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
              x => x.TaskCode.ToUpper().Contains(input.Filter.ToUpper())
              || x.Summary.ToUpper().Contains(input.Filter.ToUpper())
              || x.Status_Id.ToString().Contains(input.Filter)
              || x.CreationTime.ToString().Contains(input.Filter)
              || x.Due_Date.ToString().Contains(input.Filter)
              || x.Estimate.ToString().Contains(input.Filter)
              );
            var tatolCount = await listTask.CountAsync();
            var result = await listTask.OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var projectListDtos = ObjectMapper.Map<List<IssueListDto>>(result);

            return new PagedResultDto<IssueListDto>(
                tatolCount,
                projectListDtos
                );
        }

        public async Task<CreateIssueDto> GetId(long id)
        {
            var dto = await _issueRepository.FirstOrDefaultAsync(id);
            return ObjectMapper.Map<CreateIssueDto>(dto);
        }
        public async Task<CommonListDto> GetIssueForDetail(long id)
        {
            var task = await _issueRepository.FirstOrDefaultAsync(id);
            var issues = new List<IssueOfSprintListDto>();

            if(task.Type == 1)
            {
                return await GetSprintForManager(id);
            } else
            {
                return await GetIssueForManager(id);
            }
        }



       public async Task<CommonListDto> GetSprintForManager(long id)
        {
           var issues = await _issueRepository.GetAll().Where(x => x.Parent_Id == id).Select(x => new IssueOfSprintListDto()
            {
                Id = x.Id,
                SummaryIssue = x.Summary
            }).ToListAsync();
            var result = (from t in _issueRepository.GetAll()
                          join p in _projectRepository.GetAll()
                          on t.Parent_Id equals p.Id
                          select new CommonListDto
                          {
                              Id = t.Id,
                              Parent_Id = t.Parent_Id,
                              Assignee_Id = t.Assignee_Id,
                              CreationTime = t.CreationTime,
                              Discription = t.Discription,
                              Due_Date = t.Due_Date,
                              Estimate = t.Estimate,
                              Priority_Id = t.Priority_Id,
                              ProjectCode = p.ProjectCode,
                              Reporter_Id = t.Reporter_Id,
                              Status_Id = t.Status_Id,
                              Summary = t.Summary,
                              TaskCode = t.TaskCode,
                              TenantId = t.TenantId,
                              Type_Id = t.Type_ID,
                              ListIssue = issues,
                              Type = t.Type
                          }).Where(x => x.Id == id)
                          .FirstOrDefaultAsync();
            return result.Result;
        } 
        public async Task<CommonListDto> GetIssueForManager(long id)
        {
            var result = await _issueRepository.GetAll().Where(x => x.Id == id).Select(t => new CommonListDto
            {
                Id = t.Id,
                Parent_Id = t.Parent_Id,
                Assignee_Id = t.Assignee_Id,
                CreationTime = t.CreationTime,
                Discription = t.Discription,
                Due_Date = t.Due_Date,
                Estimate = t.Estimate,
                Priority_Id = t.Priority_Id,
                //ProjectCode = GetProjecCode(t.Type, t.Parent_Id ?? 1).Result,
                Reporter_Id = t.Reporter_Id,
                Status_Id = t.Status_Id,
                Summary = t.Summary,
                TaskCode = t.TaskCode,
                TenantId = t.TenantId,
                Type_Id = t.Type_ID,
                ListIssue = null,
                Type = t.Type
            }).FirstOrDefaultAsync();
            result.ProjectCode = GetProjecCode(result.Type, result.Parent_Id ?? 1).Result;
            return result;
        }
        public async Task<string> GetProjecCode(long type, long parentId)
        {
            if (type == 1)
            {
                // no se la sprint
                var pr =await _projectRepository.FirstOrDefaultAsync(parentId);
                return pr.ProjectCode;
            }
            else
            {
                var sprint = await _issueRepository.FirstOrDefaultAsync(parentId);
                var pr =await _projectRepository.FirstOrDefaultAsync(sprint.Parent_Id ?? 1);
                return pr.ProjectCode;
            }
        }
        public async Task DeleteIssue(long id)
        {
            await _issueRepository.DeleteAsync(id);
        }

        public async Task Update(CreateIssueDto input)
        {
            // ngày cập nhật chính là ngày hiện tại
            var dto = await _issueRepository.FirstOrDefaultAsync(input.Id);
            ObjectMapper.Map(input, dto);
        }

        public async Task StartProgress(long id)
        {
            var issue = await _issueRepository.FirstOrDefaultAsync(id);
            issue.Status_Id = 2;
        }
        public async Task StopProgress(long id)
        {
            var issue = await _issueRepository.FirstOrDefaultAsync(id);
            issue.Status_Id = 1;
        }
        public async Task Resolved(long id)
        {
            var issue = await _issueRepository.FirstOrDefaultAsync(id);
            issue.Status_Id = 3;
        }
        public async Task CloseProgress(long id)
        {
            var issue = await _issueRepository.FirstOrDefaultAsync(id);
            issue.Status_Id = 4;
        }
        public async Task ReOpenProgress(long id)
        {
            var issue = await _issueRepository.FirstOrDefaultAsync(id);
            issue.Status_Id = 5;
        }
    }
}
