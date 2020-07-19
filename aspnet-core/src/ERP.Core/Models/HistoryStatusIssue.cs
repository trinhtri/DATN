using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ERP.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERP.Models
{
    [Table("HistoryStatusIssues")]
    public class HistoryStatusIssue:FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public long  User_Id { get; set; }
        public virtual User  User_ { get; set; }
        public long  Issue_Id { get; set; }
        public virtual Issue Issue_ { get; set; }
        public string OldValue { get; set; }
        public string  NewValue { get; set; }
    }
}
