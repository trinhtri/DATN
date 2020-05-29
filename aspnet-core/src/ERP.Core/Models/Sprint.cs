using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    [Table("Sprints")]
    public class Sprint : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get ; set ; }

        [StringLength(200)]
        public string SprintName { get; set; }
        [StringLength(200)]
        public string Summary { get; set; }
        public long? Project_Id { get; set; }
        public virtual Project Project_ { get; set; }
    }
}
