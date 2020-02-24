using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Comment.Dto
{
   public class CommentListDto: EntityDto
    {
        public int? TenantId { get; set; }
        public string Discription { get; set; }
        public int Employee_Id { get; set; }
        public int Project_Id { get; set; }
        public int Issue_Id { get; set; }
    }
}
