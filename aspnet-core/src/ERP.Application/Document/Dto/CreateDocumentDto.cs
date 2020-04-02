using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Document.Dto
{
    [AutoMapTo(typeof(Models.Document))]
   public class CreateDocumentDto: EntityDto<long>
    {
        public int? TenantId { get; set; }
        public string DocumentName { get; set; }
        public string Discription { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime UploadDate { get; set; }
        public int Project_Id { get; set; }
    }
}
