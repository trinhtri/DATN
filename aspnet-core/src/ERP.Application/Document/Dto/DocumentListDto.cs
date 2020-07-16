using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Document.Dto
{
   public class DocumentListDto : EntityDto<long>
    {
        public string DocumentName { get; set; }
        public string Discription { get; set; }
        public decimal Size { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime UploadDate { get; set; }
        public string Creator { get; set; }
        public long CreatorUserId { get; set; }
    }
}