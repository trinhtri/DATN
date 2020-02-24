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
    public class Comment :FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [StringLength(2000)]
        public string Discription { get; set; }
        public int Employee_Id { get; set; }
        public virtual User Employee_ { get; set; }
        public int Project_Id { get; set; }
        public virtual Project Project_ { get; set; }
        public int Issue_Id { get; set; }
        public virtual Issue Issue_ { get; set; }
    }
}
