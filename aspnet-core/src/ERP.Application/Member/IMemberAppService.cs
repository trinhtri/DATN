using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.Dto;
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
        Task<long> Create(CreateMemberDto input);
        Task Delete(long id);
        Task Update(CreateMemberDto input);
        Task<CreateMemberDto> GetId(long id);

        Task<FileDto> GetMemberForExcel(MemberInputDto input);
    }
}
