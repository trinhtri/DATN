using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.ConfigView.Dto
{
    [AutoMapTo(typeof(Models.ConfigView))]
   public class ConfigViewDto : EntityDto<long>
    {
        public bool IsIssue { get; set; }
        public bool IsSummary { get; set; }

        public bool IsStatus { get; set; }

        public bool IsEstimate { get; set; }

        public bool IsReporter { get; set; }

        public bool IsAssignee { get; set; }

        public bool IsDueDate { get; set; }
        public bool IsCreatedDate { get; set; }
        public bool IsPriority { get; set; }
        public long UserId { get; set; }

    }
}
