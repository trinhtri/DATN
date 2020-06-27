using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.Dto;
using ERP.Issue.Dto;
using ERP.TreeView.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Issue
{
    public interface IIssueAppService : IApplicationService
    {
        Task<PagedResultDto<IssueListDto>> GetAll(IssueInputDto input);
        Task<long> Create(CreateIssueDto input);
        Task Delete(long id);
        Task Update(CreateIssueDto input);
        Task<CreateIssueDto> GetId(long id);

        Task<FileDto> GetIssueForExcel(GetTreeInputDto input);
    }
}

