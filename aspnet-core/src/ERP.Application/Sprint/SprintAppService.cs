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

namespace ERP.Sprint
{
    public class SprintAppService : ERPAppServiceBase, ISprintAppService
    {
        private readonly IRepository<Models.Sprint,long> _sprintRepository;

        public SprintAppService(IRepository<Models.Sprint, long> sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }
        public async Task<long> Create(CreateSprintDto input)
        {
            try { 
            input.TenantId = AbpSession.TenantId;
            var sprint = ObjectMapper.Map<Models.Sprint>(input);
            await _sprintRepository.InsertAsync(sprint);
            await CurrentUnitOfWork.SaveChangesAsync();
            return sprint.Id;
            } catch ( Exception e)
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
            var query = _sprintRepository.GetAll().Include(x=>x.Project_)
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
            throw new NotImplementedException();
        }

        public async Task<CreateSprintDto> GetId(long id)
        {
            var sprint = await _sprintRepository.FirstOrDefaultAsync(id);
            return ObjectMapper.Map<CreateSprintDto>(sprint);
        }

        public async Task Update(CreateSprintDto input)
        {
            var sprint = await _sprintRepository.FirstOrDefaultAsync(input.Id);
             ObjectMapper.Map(input, sprint);
        }
    }
}
