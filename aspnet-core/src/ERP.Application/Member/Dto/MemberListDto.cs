using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Member.Dto
{
    [AutoMapTo(typeof(Models.Member))]
   public class MemberListDto: EntityDto<long>
    {
        public int? TenantId { get; set; }
        public string Role { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Note { get; set; }
        public long Project_Id { get; set; }
        public string EmployeeName { get; set; }
    }
}
