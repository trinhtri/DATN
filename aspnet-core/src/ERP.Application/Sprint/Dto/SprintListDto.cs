﻿using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Sprint.Dto
{
    [AutoMapTo(typeof(Models.Sprint))]
   public class SprintListDto: Entity<long>
    {
        public int? TenantId { get; set; }
        public string SprintName { get; set; }
        public string Summary { get; set; }
        public string ProjectName { get; set; }
        public long Project_Id { get; set; }
    }
}
