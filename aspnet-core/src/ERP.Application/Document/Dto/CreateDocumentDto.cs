using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Document.Dto
{
    [AutoMapTo(typeof(Models.Document))]
   public class CreateDocumentDto: EntityDto
    {
        public int? TenantId { get; set; }
        public string DocumentName { get; set; }
        public string Discription { get; set; }
        public decimal Size { get; set; }
        public int Project_Id { get; set; }
    }
}
