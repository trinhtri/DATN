using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ERP.Authorization.Users;
using ERP.Comment.Dto;
using ERP.Member.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Comment
{
    public class CommentAppService : ERPAppServiceBase, ICommentAppService
    {
        private readonly IRepository<Models.Comment> _commentRepository;
        private readonly IRepository<User, long> _userRepository;
        public CommentAppService(
            IRepository<Models.Comment> commentRepository,
           IRepository<User, long> userRepository
            ){
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }
        public async Task<int> Create(CreateCommentDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var dto = ObjectMapper.Map<Models.Comment>(input);
            await _commentRepository.InsertAsync(dto);
            await CurrentUnitOfWork.SaveChangesAsync();
            return dto.Id;
        }

        public async Task Delete(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<CommentListDto>> GetAll(int Project_Id, int Issue_Id)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateCommentDto> GetId(int id)
        {
            var dto = await _commentRepository.FirstOrDefaultAsync(id);
            return ObjectMapper.Map<CreateCommentDto>(dto);
        }

        public async Task Update(CreateCommentDto input)
        {
            var dto = await _commentRepository.FirstOrDefaultAsync(input.Id);
            ObjectMapper.Map(input,dto);
        }
    }
}
