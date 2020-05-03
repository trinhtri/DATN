using Abp.Application.Services;
using ERP.ConfigView.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ConfigView
{
    public interface IConfigViewAppService: IApplicationService
    {
        Task<long> Create(ConfigViewDto input);
        Task Update(ConfigViewDto input);
        Task<ConfigViewDto> getId(long id);
    }
}
