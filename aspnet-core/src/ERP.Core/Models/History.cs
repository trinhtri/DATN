using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Models
{
   public class History: FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

    }
}
