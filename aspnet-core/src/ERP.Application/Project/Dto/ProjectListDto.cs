using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Project.Dto
{
    [AutoMapTo(typeof(Models.Project))]
   public class ProjectListDto: EntityDto
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ClientName { get; set; }
        public string Discription { get; set; }
        public string Note { get; set; }
        public int? TenantId { get; set; }
    }
}
