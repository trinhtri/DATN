using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ERP.Sprint.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace ERP.Sprint
{
    public class SprintAppService : ERPAppServiceBase, ISprintAppService
    {
        private readonly IRepository<Models.Sprint,long> _sprintRepository;

        public SprintAppService(IRepository<Models.Sprint, long> sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }
        public async Task<long> Create(SprintListDto input)
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

        //public async Task<PagedResultDto<SprintListDto>> GetAll(long ProjectId)
        //{
        //    var list = _sprintRepository.GetAll().Where(x => x.ProjectId == ProjectId).ToList();
        //    var
        //}

        public async Task<SprintListDto> GetId(long id)
        {
            var sprint = await _sprintRepository.FirstOrDefaultAsync(id);
            return ObjectMapper.Map<SprintListDto>(sprint);
        }

        public async Task Update(SprintListDto input)
        {
            var sprint = await _sprintRepository.FirstOrDefaultAsync(input.Id);
             ObjectMapper.Map(input, sprint);
        }
    }
}
