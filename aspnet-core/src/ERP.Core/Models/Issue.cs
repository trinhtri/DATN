using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERP.Models
{
    [Table("Tasks")]
    public class Issue : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [StringLength(100)]
        public string TaskCode { get; set; }
        [StringLength(500)]
        public string Summary { get; set; }
        public long Type_Id { get; set; }
        public long Status_Id { get; set; }
        public string Discription { get; set; }
        public long Assignee_Id { get; set; }
        public long Reporter_Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Due_Date { get; set; }
        public decimal? Estimate { get; set; }
        public long Priority_Id { get; set; }
        public long Resolve_Id { get; set; }
        public long? Sprint_Id { get; set; }
        public int Point { get; set; }
        public virtual  Sprint Sprint_ { get; set; }

    }
}
