using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ERP.Authorization.Users;
using ERP.Comment.Dto;
using ERP.Member.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Comment
{
    public class CommentAppService : ERPAppServiceBase, ICommentAppService
    {
        private readonly IRepository<Models.Comment,long> _commentRepository;
        private readonly IRepository<User, long> _userRepository;
        public CommentAppService(
            IRepository<Models.Comment,long> commentRepository,
           IRepository<User, long> userRepository
            ){
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }
        public async Task<long> Create(CreateCommentDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var dto = ObjectMapper.Map<Models.Comment>(input);
            await _commentRepository.InsertAsync(dto);
            await CurrentUnitOfWork.SaveChangesAsync();
            return dto.Id;
        }

        public async Task Delete(long id)
        {
            await _commentRepository.DeleteAsync(id);
        }

        public List<CommentListDto> GetAll(long Issue_Id)
        {
            var list = _commentRepository.GetAll().Include(x=>x.Employee_).Where(x => x.Issue_Id == Issue_Id).OrderByDescending(x => x.CreationTime);
            var dto = ObjectMapper.Map<List<CommentListDto>>(list);
            return dto;
        }

        public async Task<CreateCommentDto> GetId(long id)
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
