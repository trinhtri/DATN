using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Sprint.Dto
{
    [AutoMapTo(typeof(Models.Sprint))]
   public class SprintListDto: Entity<long>
    {
        public int? TenantId { get; set; }
        public string SprintCode { get; set; }
        public string Summary { get; set; }
        public string Discription { get; set; }

        public string ProjectName { get; set; }
        public long Project_Id { get; set; }
        public long Status_Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Due_Date { get; set; }
        public decimal? Estimate { get; set; }
        public long Assignee_Id { get; set; }
        public long Reporter_Id { get; set; }
    }
}
