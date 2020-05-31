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
        Task<PagedResultDto<SprintListDto>> GetAll(GetSprintInputDto input);
        Task<long> Create(CreateSprintDto input);
        Task Delete(long id);
        Task Update(CreateSprintDto input);
        Task<CreateSprintDto> GetId(long id);
    }
}
