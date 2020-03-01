using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Member.Dto
{
   public class CreateMemberDto: EntityDto<long>
{
    public int? TenantId { get; set; }
    public long Role_id { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Note { get; set; }
    public long Project_Id { get; set; }
    public long Employee_Id { get; set; }
    }
}