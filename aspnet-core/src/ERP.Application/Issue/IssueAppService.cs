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

        public async Task<FileDto> GetIssueActiveForExcel(IssueInputDto input)
        {
            var issueActive = await GetAll(input);
            var result = issueActive.Items.ToList();
            return _issueListExcelExport.ExportIssueToFile(result);
        }

        public async Task<FileDto> GetIssueBackLogForExcel(IssueInputDto input)
        {
            var issueActive = await GetAllIssueBackLog(input);
            var result = issueActive.Items.ToList();
            return _issueListExcelExport.ExportIssueToFile(result);
        }  
        
        public async Task<FileDto> GetIssueOfUserForExcel(IssueInputDto input)
        {
            var issues = await GetAllOfUser(input);
            var result = issues.Items.ToList();
            return _issueListExcelExport.ExportIssueToFile(result);
        }
          public async Task<FileDto> GetIssueOfSprintForExcel(IssueInputDto input, long sprintId)
        {
            var issues = await GetIssueOfSprint(input,sprintId);
            var result = issues.Items.ToList();
            return _issueListExcelExport.ExportIssueToFile(result);
        }


        public async Task<PagedResultDto<IssueListDto>> GetAll(IssueInputDto input)
        {
            var listTask = _issueRepository.GetAll().Include(x => x.Sprint_).ThenInclude(x => x.Project_)
                    .Where(x => x.Sprint_Id != null)
                    .WhereIf(input.ListStatusId != null, x => input.ListStatusId.Any(a => a == x.Status_Id))
                    .WhereIf(input.ListTypeId != null, x => input.ListTypeId.Any(a => a == x.Type_Id))
                    .WhereIf(input.ListSprintId != null, x => input.ListSprintId.Any(a => a == x.Sprint_Id))
                    .WhereIf(input.ListAssignId != null, x => input.ListAssignId.Any(a => a == x.Assignee_Id))
                    .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                  x => x.TaskCode.ToUpper().Contains(input.Filter.ToUpper())
                  || x.Summary.ToUpper().Contains(input.Filter.ToUpper())
                  || x.Status_Id.ToString().Contains(input.Filter)
                  || x.CreationTime.ToString().Contains(input.Filter)
                  || x.Due_Date.ToString().Contains(input.Filter)
                  || x.Estimate.ToString().Contains(input.Filter)
                  );
            ;
            var tatolCount = await listTask.CountAsync();
            var result = await listTask.OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var projectListDtos = ObjectMapper.Map<List<IssueListDto>>(result);
            foreach(var item in projectListDtos)
            {
                item.AssignName = GetMemberName(item.Assignee_Id);
            }

            return new PagedResultDto<IssueListDto>(
                tatolCount,
                projectListDtos
                );
        }

        public async Task<PagedResultDto<IssueListDto>> GetAllOfUser(IssueInputDto input)
        {
            var listTask = _issueRepository.GetAll().Include(x => x.Sprint_).ThenInclude(x => x.Project_)
                .Where(x => x.Sprint_Id != null)
                .Where(x=>x.Assignee_Id == AbpSession.UserId)
                    .WhereIf(input.ListStatusId != null, x => input.ListStatusId.Any(a => a == x.Status_Id))
                    .WhereIf(input.ListTypeId != null, x => input.ListTypeId.Any(a => a == x.Type_Id))
                    .WhereIf(input.ListSprintId != null, x => input.ListSprintId.Any(a => a == x.Sprint_Id))
                    .WhereIf(input.ListAssignId != null, x => input.ListAssignId.Any(a => a == x.Assignee_Id))
                    .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                  x => x.TaskCode.ToUpper().Contains(input.Filter.ToUpper())
                  || x.Summary.ToUpper().Contains(input.Filter.ToUpper())
                  || x.Status_Id.ToString().Contains(input.Filter)
                  || x.CreationTime.ToString().Contains(input.Filter)
                  || x.Due_Date.ToString().Contains(input.Filter)
                  || x.Estimate.ToString().Contains(input.Filter)
                  );
            ;
            var tatolCount = await listTask.CountAsync();
            var result = await listTask.OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var projectListDtos = ObjectMapper.Map<List<IssueListDto>>(result);
            foreach (var item in projectListDtos)
            {
                item.AssignName = GetMemberName(item.Assignee_Id);
            }

            return new PagedResultDto<IssueListDto>(
                tatolCount,
                projectListDtos
                );
        }

        public async Task<PagedResultDto<IssueListDto>> GetIssueOfSprint(IssueInputDto input, long sprintId)
        {
            var listTask = _issueRepository.GetAll().Include(x => x.Sprint_).ThenInclude(x => x.Project_)
                    .Where(x => x.Sprint_Id == sprintId)
                    .WhereIf(input.ListStatusId != null, x => input.ListStatusId.Any(a => a == x.Status_Id))
                    .WhereIf(input.ListTypeId != null, x => input.ListTypeId.Any(a => a == x.Type_Id))
                    .WhereIf(input.ListSprintId != null, x => input.ListSprintId.Any(a => a == x.Sprint_Id))
                    .WhereIf(input.ListAssignId != null, x => input.ListAssignId.Any(a => a == x.Assignee_Id))
                    .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                  x => x.TaskCode.ToUpper().Contains(input.Filter.ToUpper())
                  || x.Summary.ToUpper().Contains(input.Filter.ToUpper())
                  || x.Status_Id.ToString().Contains(input.Filter)
                  || x.CreationTime.ToString().Contains(input.Filter)
                  || x.Due_Date.ToString().Contains(input.Filter)
                  || x.Estimate.ToString().Contains(input.Filter)
                  );
            ;
            var tatolCount = await listTask.CountAsync();
            var result = await listTask.OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var projectListDtos = ObjectMapper.Map<List<IssueListDto>>(result);
            foreach (var item in projectListDtos)
            {
                item.AssignName = GetMemberName(item.Assignee_Id);
            }

            return new PagedResultDto<IssueListDto>(
                tatolCount,
                projectListDtos
                );
        }

        private string GetMemberName(long? id)
        {
            var idFake = id ?? 1;
            if (id != null)
            {
                var user = _userRepository.FirstOrDefault(x => x.Id == idFake);
                return user.UserName;
            }
            else
            {
                return "";
            }
        }

        public async Task<PagedResultDto<IssueListDto>> GetAllIssueBackLog(IssueInputDto input)
        {
            var listTask = _issueRepository.GetAll().Include(x => x.Sprint_).ThenInclude(x => x.Project_)
                    .Where(x => x.Sprint_Id == null)
                    .WhereIf(input.ListStatusId != null, x => input.ListStatusId.Any(a => a == x.Status_Id))
                    .WhereIf(input.ListTypeId != null, x => input.ListTypeId.Any(a => a == x.Type_Id))
                    .WhereIf(input.ListSprintId != null, x => input.ListSprintId.Any(a => a == x.Sprint_Id))
                    .WhereIf(input.ListAssignId != null, x => input.ListAssignId.Any(a => a == x.Assignee_Id))
                    .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                  x => x.TaskCode.ToUpper().Contains(input.Filter.ToUpper())
                  || x.Summary.ToUpper().Contains(input.Filter.ToUpper())
                  || x.Status_Id.ToString().Contains(input.Filter)
                  || x.CreationTime.ToString().Contains(input.Filter)
                  || x.Due_Date.ToString().Contains(input.Filter)
                  || x.Estimate.ToString().Contains(input.Filter)
                  );
            ;
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
            return await GetIssueForManager(id);
        }

        public async Task<CommonListDto> GetIssueForManager(long id)
        {
            var result = await _issueRepository.GetAll().Include(x => x.Sprint_).ThenInclude(b => b.Project_).Where(x => x.Id == id).Select(t => new CommonListDto
            {
                Id = t.Id,
                Parent_Id = t.Sprint_Id,
                Assignee_Id = t.Assignee_Id,
                CreationTime = t.CreationTime,
                Discription = t.Discription,
                Due_Date = t.Due_Date,
                Estimate = t.Estimate,
                Priority_Id = t.Priority_Id,
                ProjectCode = t.Sprint_.Project_.ProjectName,
                Reporter_Id = t.Reporter_Id,
                Status_Id = t.Status_Id,
                Summary = t.Summary,
                TaskCode = t.TaskCode,
                TenantId = t.TenantId,
                Type_Id = t.Type_Id,
                ListIssue = null,
            }).FirstOrDefaultAsync();
            return result;
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
        public async Task Estimate(long id, decimal time)
        {
            var issue = await _issueRepository.FirstOrDefaultAsync(id);
            issue.Estimate = time;
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


        public async Task<List<IssueListOfSprintDto>> GetIssuesStatusOfSprint(long id, List<long> listId)
        {
            var issue = await _issueRepository.GetAll().Where(x => x.Sprint_Id == id).WhereIf(listId.Count > 0, x=>listId.Any(a=>a == x.Status_Id)).OrderBy("TaskCode").ToListAsync();
            var dto = ObjectMapper.Map<List<IssueListOfSprintDto>>(issue);
            foreach(var  item in dto)
            {
                item.AssignName = GetMemberName(item.Assignee_Id);
            }
            return dto;
        }   
        
        public async Task<List<IssueListOfSprintDto>> GetIssuesTypeOfSprint(long id, List<long> listId)
        {
            var issue = await _issueRepository.GetAll().Where(x => x.Sprint_Id == id).WhereIf(listId.Count > 0, x=>listId.Any(a=>a == x.Type_Id)).OrderBy("TaskCode").ToListAsync();
            var dto = ObjectMapper.Map<List<IssueListOfSprintDto>>(issue);
            foreach(var  item in dto)
            {
                item.AssignName = GetMemberName(item.Assignee_Id);
            }
            return dto;
        }

        public async Task<ScaleStatusIssueOfSprintForChart> GetScaleStatusForChart(long sprintId)
        {
            // lấy hết time ra
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == sprintId).Sum(x => x.Point);
            if (totalTime == 0 || totalTime == null)
            {
                totalTime = 1;
            }
            var ScaleOpen = GetPointOpen(sprintId);
            var ScaleInProgress = GetPointInProgress(sprintId);
            var ScaleResolved = GetPointResolved(sprintId);
            var ScaleCompeleted = GetPointCompeleted(sprintId);
            var ScaleReOpened = GetPointReOpened(sprintId);

            var result = new ScaleStatusIssueOfSprintForChart();
            result.ScaleOpen = Convert.ToInt32((ScaleOpen / totalTime) * 100);
            result.ScaleCompeleted = Convert.ToInt32((ScaleCompeleted / totalTime) * 100);
            result.ScaleInProgress = Convert.ToInt32((ScaleInProgress / totalTime) * 100);
            result.ScaleResolved = Convert.ToInt32((ScaleResolved / totalTime) * 100);
            result.ScaleReOpened = Convert.ToInt32((ScaleReOpened / totalTime) * 100);
            return result;
        }


        public decimal GetPointOpen(long id)
        {
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == id && x.Status_Id == 1).Sum(x => x.Point);
            return totalTime;
        }
        public decimal GetPointInProgress(long id)
        {
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == id && x.Status_Id == 2).Sum(x => x.Point);
            return totalTime;
        }
        public decimal GetPointResolved(long id)
        {
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == id && x.Status_Id == 3).Sum(x => x.Point);
            return totalTime;
        }
        public decimal GetPointCompeleted(long id)
        {
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == id && x.Status_Id == 4).Sum(x => x.Point);
            return totalTime;
        }
        public decimal GetPointReOpened(long id)
        {
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == id && x.Status_Id == 5).Sum(x => x.Point);
            return totalTime;
        }
        public async Task<ScaleTypeIssueOfSprintForChart> GetScaleTypeForChart(long sprintId)
        {
            // lấy hết time ra
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == sprintId).Sum(x => x.Point);
            if (totalTime == 0 || totalTime == null)
            {
                totalTime = 1;
            }
            var ScaleNewFeature = GetPointNewFeatureForType(sprintId);
            var ScaleImprovent = GetPointImprovementForType(sprintId);
            var ScaleBug = GetPointBugForType(sprintId);

            var result = new ScaleTypeIssueOfSprintForChart();
            result.ScaleNewFeature = Convert.ToInt32((ScaleNewFeature / totalTime) * 100);
            result.ScaleImprovent = Convert.ToInt32((ScaleImprovent / totalTime) * 100);
            result.ScaleBug = Convert.ToInt32((ScaleBug / totalTime) * 100);

            return result;
        }

        public decimal GetPointNewFeatureForType(long id)
        {
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == id && x.Type_Id == 1).Sum(x => x.Point);
            return totalTime;
        }
        public decimal GetPointImprovementForType(long id)
        {
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == id && x.Type_Id == 2).Sum(x => x.Point);
            return totalTime;
        }
        public decimal GetPointBugForType(long id)
        {
            var totalTime = _issueRepository.GetAll().Where(x => x.Sprint_Id == id && x.Type_Id == 3).Sum(x => x.Point);
            return totalTime;
        }

    }
}
