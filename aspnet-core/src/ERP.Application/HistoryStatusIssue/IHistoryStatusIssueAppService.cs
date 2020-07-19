using Abp.Application.Services;
using ERP.HistoryStatusIssue.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.HistoryStatusIssue
{
    public interface IHistoryStatusIssueAppService : IApplicationService
    {
        Task<long> Create(CreateHistoryStatusIssueDto input);
        Task<List<HistoryStatusIssueListDto>> GetAll(long issueId);
    }
}
