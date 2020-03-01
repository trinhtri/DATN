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
        public string IssueCode { get; set; }
        public string IssueName { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string Discription { get; set; }
        public long Project_Id { get; set; }
        public long Assignee_Id { get; set; }
        public long Reporter_Id { get; set; }
        public DateTime? Due_Date { get; set; }
        public DateTime? Update_Date { get; set; }
        public DateTime? Resolved_Date { get; set; }
    }
}
