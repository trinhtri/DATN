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

namespace ERP.Issue
{
    public class IssueAppService : ERPAppServiceBase, IIssueAppService
    {
        private readonly IRepository<Models.Issue,long> _issueRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IssueListExcelExporter _issueListExcelExport;
        private readonly IRepository<Models.Project, long> _projectRepository;

        public IssueAppService(IRepository<Models.Issue,long> issueRepository,
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

        public async Task<FileDto> GetIssueForExcel(IssueInputDto input)
        {
            var list = await GetAll(input);
            var dto = list.Items.ToList();
            return _issueListExcelExport.ExportToFile(dto);
        }

        public async Task<PagedResultDto<IssueListDto>> GetAll(IssueInputDto input)
        {
            var list = _issueRepository.GetAll()
                .Include(x=>x.Project_)
                .Where(x=>x.Type == input.Type)
                .WhereIf(input.ListStatusId !=null, x=>input.ListStatusId.Any(a=>a == x.Status_Id))
                .WhereIf(input.ListTypeId != null, x => input.ListTypeId.Any(a => a == x.Type_ID))
                .WhereIf(input.ListAssignId != null, x => input.ListAssignId.Any(a => a == x.Assignee_Id))
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
              x => x.IssueCode.ToUpper().Contains(input.Filter.ToUpper())
              || x.Summary.ToUpper().Contains(input.Filter.ToUpper())
              || x.Status_Id.ToString().Contains(input.Filter)
              || x.CreationTime.ToString().Contains(input.Filter)
              || x.Due_Date.ToString().Contains(input.Filter)
              || x.Estimate.ToString().Contains(input.Filter)
              );
            var tatolCount = await list.CountAsync();
            var result = await list.OrderBy(input.Sorting)
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
        public async Task<IssueListDto> GetIssueForDetail(long id)
        {
            var dto = await _issueRepository.GetAll()
                .Include(x=>x.Project_)
                .Where(x=>x.Id == id)
                .FirstOrDefaultAsync();
            return ObjectMapper.Map<IssueListDto>(dto);
        }

        public async Task Update(CreateIssueDto input)
        {
            // ngày cập nhật chính là ngày hiện tại
            input.Update_Date = DateTime.Now;
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
