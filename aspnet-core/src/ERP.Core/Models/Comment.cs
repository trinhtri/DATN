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
    [Table("Comments")]
    public class Comment :FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [StringLength(2000)]
        public string Discription { get; set; }
        public long Employee_Id { get; set; }
        public virtual User Employee_ { get; set; }
        public long Issue_Id { get; set; }
        public virtual Issue Issue_ { get; set; }
    }
}
