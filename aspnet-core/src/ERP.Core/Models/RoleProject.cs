using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERP.Models
{
    [Table("RoleProject")]
    public class RoleProject : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [StringLength(2000)]
        public string RoleName { get; set; }
    }
}
    