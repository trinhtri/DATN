using Abp.Runtime.Validation;
using ERP.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Member.Dto
{
   public class MemberInputDto :PagedAndSortedInputDto, IShouldNormalize
    {
        public int Project_Id { get; set; }
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "EffectiveDate,EndDate,StartDate,Note,EmployeeName,Role";
            }

            Filter = Filter?.Trim();
        }
    }
}
