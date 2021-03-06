﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Project.Dto
{
    [AutoMapTo(typeof(Models.Project))]
   public class ProjectListDto: EntityDto<long>
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ClientName { get; set; }
        public string Discription { get; set; }
        public string Note { get; set; }
        public int? TenantId { get; set; }
         public bool Status { get; set; }
        public long Assignee_Id { get; set; }
        public long Reporter_Id { get; set; }
    }
}
