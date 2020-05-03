using Abp.Domain.Repositories;
using ERP.ConfigView.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ConfigView
{
    public class ConfigviewAppService : ERPAppServiceBase, IConfigViewAppService
    {
        private readonly IRepository<Models.ConfigView, long> _configViewRepository;
        public ConfigviewAppService(IRepository<Models.ConfigView, long> configViewRepository)
        {
            _configViewRepository = configViewRepository;
        }
        public async Task<long> Create(ConfigViewDto input)
        {
            var configView = ObjectMapper.Map<Models.ConfigView>(input);
            await _configViewRepository.InsertAsync(configView);
            await CurrentUnitOfWork.SaveChangesAsync();
            return configView.Id;
        }

        public async Task<ConfigViewDto> getId(long id)
        {
            var config = await _configViewRepository.FirstOrDefaultAsync(x=>x.UserId == id);
            return ObjectMapper.Map<ConfigViewDto>(config);
        }

        public async Task Update(ConfigViewDto input)
        {
            var config = await _configViewRepository.FirstOrDefaultAsync(input.Id);
            ObjectMapper.Map(input, config);
        }
    }
}
