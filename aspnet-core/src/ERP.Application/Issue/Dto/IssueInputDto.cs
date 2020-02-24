using Abp.Runtime.Validation;
using ERP.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Issue.Dto
{
   public class IssueInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int Project_Id { get; set; }
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "IssueCode,IssueName,Type,Status,Due_Date";
            }

            Filter = Filter?.Trim();
        }
    }
}

