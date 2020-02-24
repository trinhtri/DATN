using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using ERP.Authorization.Users;
using ERP.Issue.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;

namespace ERP.Issue
{
    public class IssueAppService : ERPAppServiceBase, IIssueAppService
    {
        private readonly IRepository<Models.Issue> _issueRepository;
        private readonly IRepository<User, long> _userRepository;
        public IssueAppService(IRepository<Models.Issue> issueRepository,
            IRepository<User, long> userRepository
            )
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
        }
        public async Task<int> Create(CreateIssueDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var issue = ObjectMapper.Map<Models.Issue>(input);
            await _issueRepository.InsertAsync(issue);
            await CurrentUnitOfWork.SaveChangesAsync();
            return issue.Id;
        }

        public async Task Delete(int id)
        {
            await _issueRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<IssueListDto>> GetAll(IssueInputDto input)
        {
            var list = _issueRepository.GetAll().WhereIf(!input.Filter.IsNullOrWhiteSpace(),
              x => x.IssueCode.ToUpper().Contains(input.Filter.ToUpper())
              || x.IssueName.ToUpper().Contains(input.Filter.ToUpper())
              || x.Status.ToString().Contains(input.Filter)
              || x.CreationTime.ToString().Contains(input.Filter)
              || x.Due_Date.ToString().Contains(input.Filter)
              || x.Estimate.ToString().Contains(input.Filter)
              );
            var tatolCount = await list.AsQueryable().CountAsync();
            var result = await list.AsQueryable().OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var projectListDtos = ObjectMapper.Map<List<IssueListDto>>(result);

            return new PagedResultDto<IssueListDto>(
                tatolCount,
                projectListDtos
                );
        }

        public async Task<CreateIssueDto> GetId(int id)
        {
            var dto = await _issueRepository.FirstOrDefaultAsync(id);
            return ObjectMapper.Map<CreateIssueDto>(dto);
        }

        public async Task Update(CreateIssueDto input)
        {
            throw new NotImplementedException();
        }
    }
}
