using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ERP.Sprint.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Abp.Collections.Extensions;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using ERP.Issue.Exporting;
using ERP.Dto;
using ERP.Sprint.Exporting;

namespace ERP.Sprint
{
    public class SprintAppService : ERPAppServiceBase, ISprintAppService
    {
        private readonly IRepository<Models.Sprint, long> _sprintRepository;
        private readonly IRepository<Models.Issue, long> _issueRepository;
        private SprintListExcelExporter _sprintListExcelExporter;

        public SprintAppService(IRepository<Models.Sprint, long> sprintRepository, IRepository<Models.Issue, long> issueRepository, SprintListExcelExporter sprintListExcelExporter)
        {
            _sprintRepository = sprintRepository;
            _issueRepository = issueRepository;
            _sprintListExcelExporter = sprintListExcelExporter;
        }
        public async Task<long> Create(CreateSprintDto input)
        {
            try
            {
                input.TenantId = AbpSession.TenantId;
                var sprint = ObjectMapper.Map<Models.Sprint>(input);
                await _sprintRepository.InsertAsync(sprint);
                await CurrentUnitOfWork.SaveChangesAsync();
                return sprint.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Delete(long id)
        {
            await _sprintRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<SprintListDto>> GetAll(GetSprintInputDto input)
        {
            var query = _sprintRepository.GetAll().Include(x => x.Project_)
                    .WhereIf(input.ListStatusId != null, x => input.ListStatusId.Any(a => a == x.Status_Id))
                    .WhereIf(input.ListProjectId != null, x => input.ListProjectId.Any(a => a == x.Project_Id))
                    .WhereIf(input.ListAssignId != null, x => input.ListAssignId.Any(a => a == x.Assignee_Id))
                .WhereIf(!string.IsNullOrEmpty(input.Filter),
                x => x.SprintCode.ToUpper().Contains(input.Filter.ToUpper())
                || x.Summary.ToUpper().Contains(input.Filter.ToUpper())
                || x.Project_.ProjectName.ToUpper().Contains(input.Filter.ToUpper()));
            var totalCount = await query.AsQueryable().CountAsync();

            var result = await query.AsQueryable().OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var output = ObjectMapper.Map<List<SprintListDto>>(result);

            return new PagedResultDto<SprintListDto>(
                totalCount,
                output
                );
        }

        public async Task<PagedResultDto<SprintListDto>> GetAllSprintOfProject(GetSprintInputDto input, long projectId)
        {
            var query = _sprintRepository.GetAll().Include(x => x.Project_)
                .Where(x=>x.Project_Id == projectId)
                    .WhereIf(input.ListStatusId != null, x => input.ListStatusId.Any(a => a == x.Status_Id))
                    .WhereIf(input.ListProjectId != null, x => input.ListProjectId.Any(a => a == x.Project_Id))
                    .WhereIf(input.ListAssignId != null, x => input.ListAssignId.Any(a => a == x.Assignee_Id))
                .WhereIf(!string.IsNullOrEmpty(input.Filter),
                x => x.SprintCode.ToUpper().Contains(input.Filter.ToUpper())
                || x.Summary.ToUpper().Contains(input.Filter.ToUpper())
                || x.Project_.ProjectName.ToUpper().Contains(input.Filter.ToUpper()));
            var totalCount = await query.AsQueryable().CountAsync();

            var result = await query.AsQueryable().OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var output = ObjectMapper.Map<List<SprintListDto>>(result);

            return new PagedResultDto<SprintListDto>(
                totalCount,
                output
                );
        }


        public async Task<CreateSprintDto> GetId(long id)
        {
            var total = _issueRepository.GetAll().Where(x => x.Sprint_Id == id).Sum(a => a.Estimate);
            var sprint = await _sprintRepository.FirstOrDefaultAsync(id);
            sprint.Estimate = total;
            return ObjectMapper.Map<CreateSprintDto>(sprint);
        }

        public async Task Update(CreateSprintDto input)
        {
            var sprint = await _sprintRepository.FirstOrDefaultAsync(input.Id);
            ObjectMapper.Map(input, sprint);
        }

        public async Task<FileDto> GetSprintForExcel(GetSprintInputDto input)
        {
            var issueActive = await GetAll(input);
            var result = issueActive.Items.ToList();
            return _sprintListExcelExporter.ExportToFile(result);
        }
          public async Task<FileDto> GetSprintOfProjectForExcel(GetSprintInputDto input, long projectId)
        {
            var issueActive = await GetAllSprintOfProject(input, projectId);
            var result = issueActive.Items.ToList();
            return _sprintListExcelExporter.ExportToFile(result);
        }


        public async Task ActiveSprint(long input)
        {
            var sprint = await _sprintRepository.FirstOrDefaultAsync(input);
            sprint.Status_Id = 1;

        }

        public async Task CloseSprint(long input)
        {
            var sprint = await _sprintRepository.FirstOrDefaultAsync(input);
            sprint.Status_Id = 3;

            // lấy tất cả các issue của sprint này ra 
            var issues = await _issueRepository.GetAll().Where(x => x.Sprint_Id == input).ToListAsync();
            foreach (var item in issues)
            {
                if (item.Status_Id != 4)
                    item.Sprint_Id = null;
            }
        }

        public async Task CancelSprint(long input)
        {
            var sprint = await _sprintRepository.FirstOrDefaultAsync(input);
            sprint.Status_Id = 4;

            // lấy tất cả các issue của sprint này ra 
            var issues = await _issueRepository.GetAll().Where(x => x.Sprint_Id == input).ToListAsync();
            foreach (var item in issues)
            {
                if (item.Status_Id != 4)
                    item.Sprint_Id = null;
            }
        }


    }
}
