using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERP.Models
{
    [Table("Documents")]
   public class Document: FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [StringLength(100)]
        public string DocumentName { get; set; }
        [StringLength(2000)]
        public string Discription { get; set; }
        public decimal Size { get; set; }
        public long Project_Id { get; set; }
        public virtual Project Project { get; set; }
    }
}
