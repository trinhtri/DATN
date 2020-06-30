using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    [Table("Sprints")]
    public class Sprint : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get ; set ; }

        [StringLength(200)]
        public string SprintCode { get; set; }
        [StringLength(2000)]
        public string Summary { get; set; }
        public string Discription { get; set; }
        public long? Project_Id { get; set; }
        public virtual Project Project_ { get; set; }
        public long Status_Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Due_Date { get; set; }
        public decimal? Estimate { get; set; }
        public long Assignee_Id { get; set; }
        public long Reporter_Id { get; set; }

    }
}
