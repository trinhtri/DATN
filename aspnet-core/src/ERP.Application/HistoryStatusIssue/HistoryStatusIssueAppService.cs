using Abp.Domain.Repositories;
using ERP.HistoryStatusIssue.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.HistoryStatusIssue
{
    public class HistoryStatusIssueAppService : ERPAppServiceBase, IHistoryStatusIssueAppService
    {
        private readonly IRepository<Models.HistoryStatusIssue, long> _historyRepository; 
        public HistoryStatusIssueAppService(IRepository<Models.HistoryStatusIssue, long> historyRepository)
        {
            _historyRepository = historyRepository;
        }
        public async Task<long> Create(CreateHistoryStatusIssueDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var history = ObjectMapper.Map<Models.HistoryStatusIssue>(input);
            var id =await _historyRepository.InsertAndGetIdAsync(history);
            await CurrentUnitOfWork.SaveChangesAsync();
            return id;
        }

        public async Task<List<HistoryStatusIssueListDto>> GetAll(long issueId)
        {
            var list =await _historyRepository.GetAll().Include(x => x.Issue_).Include(x => x.User_).Where(x => x.Issue_Id == issueId).ToListAsync();
            var ouput = ObjectMapper.Map<List<HistoryStatusIssueListDto>>(list);
            return ouput;
        }
    }
}
