using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Comment.Dto
{
    [AutoMapTo(typeof(Models.Comment))]
   public class CommentListDto: EntityDto<long>
    {
        public string Discription { get; set; }
        public string EmployeeName { get; set; }
        public long Issue_Id { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
