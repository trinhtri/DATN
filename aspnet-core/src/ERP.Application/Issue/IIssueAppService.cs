using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.Issue.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Issue
{
    public interface IIssueAppService : IApplicationService
    {
        Task<PagedResultDto<IssueListDto>> GetAll(IssueInputDto input);
        Task<int> Create(CreateIssueDto input);
        Task Delete(int id);
        Task Update(CreateIssueDto input);
        Task<CreateIssueDto> GetId(int id);
    }
}

