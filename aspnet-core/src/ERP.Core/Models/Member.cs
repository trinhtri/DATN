using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ERP.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERP.Models
{
    [Table("Members")]
    public class Member: FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public int Role { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(2000)]
        public string Note { get; set; }
        public int Project_Id { get; set; }
        public virtual Project Project_ { get; set; }
        public int Employee_Id { get; set; }
        public virtual User _Employee_ { get; set; }
    }
}
