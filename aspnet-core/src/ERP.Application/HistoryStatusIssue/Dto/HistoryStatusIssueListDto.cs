using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.HistoryStatusIssue.Dto
{
    public class HistoryStatusIssueListDto : Entity<long>
    {
        public int? TenantId { get; set; }
        public string UserName { get; set; }
        public long Issue_Id { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
