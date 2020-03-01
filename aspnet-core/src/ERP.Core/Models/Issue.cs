using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ERP.Models
{
    public class Issue : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [StringLength(100)]
        public string IssueCode { get; set; }
        [StringLength(100)]
        public string IssueName { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        [StringLength(2000)]
        public string Discription { get; set; }

        public int Project_Id { get; set; }
        public virtual Project Project_ { get; set; }
        public long Assignee_Id { get; set; }
        public long Reporter_Id { get; set; }
        public DateTime? Due_Date { get; set; }
        public DateTime? Update_Date { get; set; }
        public DateTime? Resolved_Date { get; set; }
        public decimal? Estimate { get; set; }
    }
}
