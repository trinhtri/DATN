using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Issue.Dto
{
    [AutoMapTo(typeof(Models.Issue))]
    public class IssueListDto:EntityDto
    {
        public int TenantId { get; set; }
        public string IssueCode { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Discription { get; set; }
        public long Project_Id { get; set; }
        public long Assignee_Id { get; set; }
        public long Reporter_Id { get; set; }
        public DateTime? Due_Date { get; set; }
        public DateTime? Update_Date { get; set; }
        public DateTime? Resolved_Date { get; set; }
        public decimal? Estimate { get; set; }
        public string Priority { get; set; }
        public string ProjectCode { get; set; }
        public string Resolve { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
    