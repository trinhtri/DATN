using Abp.Application.Services;
using ERP.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared
{
   public interface ICommonAppService: IApplicationService
    {
        Task<List<ERPComboboxItem>> GetLookups(string type, int? tenantId = null, int? parentId = null);

    }
}
