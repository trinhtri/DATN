using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.HistoryStatusIssue.Dto
{
    public class CreateHistoryStatusIssueDto : Entity<long>
    {
        public int? TenantId { get; set; }
        public long User_Id { get; set; }
        public long Issue_Id { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
