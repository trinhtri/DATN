using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Comment.Dto
{
    [AutoMapTo(typeof(Models.Comment))]
   public class CreateCommentDto: EntityDto
    {
        public int? TenantId { get; set; }
        public string Discription { get; set; }
        public int Employee_Id { get; set; }
        public int Project_Id { get; set; }
        public int Issue_Id { get; set; }
    }
}
