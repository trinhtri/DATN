using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ERP.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERP.Models
{
    [Table("ConfigView")]

    public class ConfigView: FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [DefaultValue("true")]
        public bool IsIssue { get; set; }
        [DefaultValue("true")]
        public bool IsSummary { get; set; }

        public bool IsStatus { get; set; }

        public bool IsEstimate { get; set; }

        public bool IsReporter  { get; set; }

        public bool IsAssignee { get; set; }

        public bool IsDueDate  { get; set; }
        [DefaultValue("true")]
        public bool IsCreatedDate { get; set; }
        public bool IsPriority { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }


    }
}
