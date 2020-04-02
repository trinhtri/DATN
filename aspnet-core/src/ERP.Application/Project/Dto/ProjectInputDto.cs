using Abp.Runtime.Validation;
using ERP.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Project.Dto
{
   public class ProjectInputDto :PagedAndSortedInputDto, IShouldNormalize
    {
    public string Filter { get; set; }
    public bool? Status { get; set; }
    public void Normalize()
    {
        if (string.IsNullOrEmpty(Sorting))
        {
            Sorting = "ProjectCode,ProjectName,StartDate,ClientName,Note,EndDate";
        }

        Filter = Filter?.Trim();
    }
}
}
