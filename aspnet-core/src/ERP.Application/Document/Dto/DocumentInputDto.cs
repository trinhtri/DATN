using Abp.Runtime.Validation;
using ERP.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Document.Dto
{
   public class DocumentInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int Project_Id { get; set; }
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "DocumentName,Discription,Size,UploadDate";
            }

            Filter = Filter?.Trim();
        }
    }
}