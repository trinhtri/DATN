using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ERP.Project.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ERP.Project
{
    public class ProjectAppService : ERPAppServiceBase, IProjectAppService
    {
        private readonly IRepository<Models.Project> _projectRepository;
        public ProjectAppService(IRepository<Models.Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<int> Create(CreateProjectDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var project = ObjectMapper.Map<Models.Project>(input);
            await _projectRepository.InsertAsync(project);
            await CurrentUnitOfWork.SaveChangesAsync();
            return project.Id;
        }

        public async Task Delete(int id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        public async Task<CreateProjectDto> GetId(int id)
        {
            var dto = await _projectRepository.FirstOrDefaultAsync(id);
            return ObjectMapper.Map<CreateProjectDto>(dto);
        }

        public async Task<PagedResultDto<ProjectListDto>> GetAll(ProjectInputDto input)
        {
            var list = _projectRepository.GetAll().WhereIf(!input.Filter.IsNullOrWhiteSpace(),
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
    }
}
