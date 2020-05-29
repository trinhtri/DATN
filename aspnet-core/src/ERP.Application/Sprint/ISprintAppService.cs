using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.Sprint.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Sprint
{
  public  interface ISprintAppService: IApplicationService
    {
        //Task<PagedResultDto<SprintListDto>> GetAll(long ProjectId);
        Task<long> Create(SprintListDto input);
        Task Delete(long id);
        Task Update(SprintListDto input);
        Task<SprintListDto> GetId(long id);
    }
}
