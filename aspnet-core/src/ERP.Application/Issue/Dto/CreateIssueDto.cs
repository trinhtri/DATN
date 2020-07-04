using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Issue.Dto
{
    [AutoMapTo(typeof(Models.Issue))]
   public class CreateIssueDto:EntityDto<long>
    {
        public int? TenantId { get; set; }
        public string TaskCode { get; set; }
        public string Summary { get; set; }     
        public long Type_ID { get; set; }
        public long Status_Id { get; set; }
        public string Discription { get; set; }
        public long Assignee_Id { get; set; }
        public long Reporter_Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Due_Date { get; set; }
        public long Priority_ID { get; set; }
        public long Resolve_Id { get; set; }
        public decimal? Estimate { get; set; }
        public int Point { get; set; }
        public long? Sprint_Id { get; set; }

    }
}
