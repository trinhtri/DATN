using Abp.Runtime.Validation;
using ERP.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Issue.Dto
{
   public class IssueInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public List<long> ListSprintId { get; set; }
        public List<long> ListStatusId { get; set; }
        public List<long> ListAssignId { get; set; }
        public List<long> ListTypeId { get; set; }
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "TaskCode,Summary";
            }

            Filter = Filter?.Trim();
        }
    }
}

