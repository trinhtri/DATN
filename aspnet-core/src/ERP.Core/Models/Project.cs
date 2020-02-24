using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERP.Models
{
    [Table("Projects")]
    public class Project: FullAuditedEntity, IMustHaveTenant
    { 
        [MaxLength(100)]
        public string ProjectCode { get; set; }
        [StringLength(500)]
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(100)]
        public string ClientName { get; set; }
        [StringLength(2000)]
        public string Discription { get; set; }
        [StringLength(2000)]
        public string Note { get; set; }
        public int TenantId { get; set; }
    }
}
