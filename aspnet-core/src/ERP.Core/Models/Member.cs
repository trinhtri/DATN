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
    public class Member: FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long Role_id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(2000)]
        public string Note { get; set; }
        public long Project_Id { get; set; }
        public virtual Project Project_ { get; set; }
        public long Employee_Id { get; set; }
        public virtual User Employee_ { get; set; }
    }
}
