using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ERP.Dto;
using ERP.Project.Dto;
using ERP.Project.Exporting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ERP.Project
{
    public class ProjectAppService : ERPAppServiceBase, IProjectAppService
    {
        private readonly IRepository<Models.Project,long> _projectRepository;
        private readonly ProjectListExcelExporter _projectListExcelExporter;
        private readonly IRepository<Models.Member, long> _memberRepository;

        public ProjectAppService(IRepository<Models.Project,long> projectRepository,
            ProjectListExcelExporter projectListExcelExporter,
            IRepository<Models.Member, long> memberRepository)
        {
            _projectRepository = projectRepository;
            _projectListExcelExporter = projectListExcelExporter;
            _memberRepository = memberRepository;
        }
        public async Task<long> Create(CreateProjectDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var project = ObjectMapper.Map<Models.Project>(input);
            await _projectRepository.InsertAsync(project);
            await CurrentUnitOfWork.SaveChangesAsync();
            return project.Id;
        }

        public async Task Delete(long id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        public async Task<CreateProjectDto> GetId(long id)
        {
            var dto = await _projectRepository.FirstOrDefaultAsync(id);
            return ObjectMapper.Map<CreateProjectDto>(dto);
        }

        public async Task<PagedResultDto<ProjectListDto>> GetAll(ProjectInputDto input)
        {
            var projectOfMember = _memberRepository.GetAll().Where(x => x.Employee_Id == AbpSession.UserId).ToList();
            var list = _projectRepository.GetAll()
                .Where(x=> projectOfMember.Any(p=>p.Project_Id == x.Id))
                .WhereIf(input.Status.HasValue,x=>x.Status == input.Status)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                x => x.ProjectCode.ToUpper().Contains(input.Filter.ToUpper())
                || x.ProjectName.ToUpper().Contains(input.Filter.ToUpper())
                || x.StartDate.ToString().Contains(input.Filter));

            var tatolCount = await list.CountAsync();
            var result = await list.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var projectListDtos = ObjectMapper.Map<List<ProjectListDto>>(result);

            return new PagedResultDto<ProjectListDto>(
                tatolCount,
                projectListDtos
                );
        }

        public async Task Update(CreateProjectDto input)
        {
            var dto = await _projectRepository.FirstOrDefaultAsync(input.Id);
            ObjectMapper.Map(input, dto);
        }

        public async Task<FileDto> GetProjectToExcel(ProjectInputDto inputDto)
        {
            var list = await GetAll(inputDto);
            var dto = list.Items.ToList();
            return _projectListExcelExporter.ExportToFile(dto);
        }
    }
}
