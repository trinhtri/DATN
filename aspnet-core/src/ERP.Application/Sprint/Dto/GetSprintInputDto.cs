using Abp.Runtime.Validation;
using ERP.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Sprint.Dto
{
    public class GetSprintInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public List<long> ListProjectId { get; set; }
        public List<long> ListStatusId { get; set; }
        public List<long> ListAssignId { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Status_Id";
            }

            Filter = Filter?.Trim();
        }
    }
}
