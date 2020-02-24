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
        Task<PagedResultDto<CommentListDto>> GetAll(int Project_Id,int Issue_Id);
        Task<int> Create(CreateCommentDto input);
        Task Delete(int id);
        Task Update(CreateCommentDto input);
        Task<CreateCommentDto> GetId(int id);
    }
}

