using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.Member.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Member
{
    public interface IMemberAppService : IApplicationService
    {
        Task<PagedResultDto<MemberListDto>> GetAll(MemberInputDto input);
        Task<int> Create(CreateMemberDto input);
        Task Delete(int id);
        Task Update(CreateMemberDto input);
        Task<CreateMemberDto> GetId(int id);
    }
}
