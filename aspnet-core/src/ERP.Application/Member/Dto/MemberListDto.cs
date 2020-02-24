using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Member.Dto
{
    [AutoMapTo(typeof(Models.Member))]
   public class MemberListDto: EntityDto
    {
        public int? TenantId { get; set; }
        public int Role { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Note { get; set; }
        public int Project_Id { get; set; }
        public string EmployeeName { get; set; }
    }
}
