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
    public void Normalize()
    {
        if (string.IsNullOrEmpty(Sorting))
        {
            Sorting = "SprintName, Summary";
        }

        Filter = Filter?.Trim();
    }
}
}
