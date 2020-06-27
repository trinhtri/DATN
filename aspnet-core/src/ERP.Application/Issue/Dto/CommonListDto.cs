using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Issue.Dto
{
   public class CommonListDto: Entity<long>
    {
        public int TenantId { get; set; }
        public string IssueCode { get; set; }
        public string Summary { get; set; }
        public long Type_Id { get; set; }
        public long Status_Id { get; set; }
        public string Discription { get; set; }
        //public long Sprint_Id { get; set; }
        public long Assignee_Id { get; set; }
        public long Reporter_Id { get; set; }
        public DateTime? Due_Date { get; set; }
        public DateTime? Update_Date { get; set; }
        public DateTime? Resolved_Date { get; set; }
        public decimal? Estimate { get; set; }
        public long Priority_Id { get; set; }
        public string ProjectCode { get; set; }
        public long? Project_Id { get; set; }
        public DateTime CreationTime { get; set; }
        public List<IssueOfSprintListDto> ListIssue { get; set; }
        public long? Parent_Id { get; set; }


        public CommonListDto()
        {
            ListIssue = new List<IssueOfSprintListDto>();
        }
    }
}
