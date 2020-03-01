using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.Comment.Dto;
using ERP.Member.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Comment
{
    public interface ICommentAppService: IApplicationService
    {
        Task<PagedResultDto<CommentListDto>> GetAll(long Project_Id,long Issue_Id);
        Task<long> Create(CreateCommentDto input);
        Task Delete(long id);
        Task Update(CreateCommentDto input);
        Task<CreateCommentDto> GetId(long id);
    }
}

