using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Issue.Dto
{
   public class IssueOfSprintListDto: Entity<long>
    {
        public string SummaryIssue { get; set; }
    }
}
