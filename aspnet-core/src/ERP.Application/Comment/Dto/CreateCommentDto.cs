using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Comment.Dto
{
    [AutoMapTo(typeof(Models.Comment))]
   public class CreateCommentDto: EntityDto<long>
    {
        public int? TenantId { get; set; }
        public string Discription { get; set; }
        public long Employee_Id { get; set; }
        public long Issue_Id { get; set; }
    }
}
