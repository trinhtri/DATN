using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERP.Models
{
    [Table("Status")]

    public class Status:FullAuditedEntity<long>, IMustHaveTenant
    {
     public int TenantId { get; set; }
    [StringLength(200)]
    public string StatusName { get; set; }
}
}

