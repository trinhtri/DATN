using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ERP.Document.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Document
{
    public class DocumentAppService : ERPAppServiceBase, IDocumentAppService
    {
        private readonly IRepository<Models.Document,long> _documentRepository;
        public DocumentAppService(IRepository<Models.Document,long> documentRepository)
        {
            _documentRepository = documentRepository;
        }
        public async Task<long> Create(CreateDocumentDto input)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
           await _documentRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<DocumentListDto>> GetAll(DocumentInputDto input)
        {
            var list = await _documentRepository.GetAll().Where(x => x.Project_Id == input.Project_Id).ToListAsync();
            var tatolCount = await list.AsQueryable().CountAsync();
            var result = await list.AsQueryable().OrderBy(input.Sorting)
                .PageBy(input).ToListAsync();
            var dto = ObjectMapper.Map<List<DocumentListDto>>(result);
            return new PagedResultDto<DocumentListDto>(tatolCount, dto);
        }

        public async Task<CreateDocumentDto> GetId(long id)
        {
            var document = await _documentRepository.FirstOrDefaultAsync(id);
            return ObjectMapper.Map<CreateDocumentDto>(document);
        }

        public async Task Update(CreateDocumentDto input)
        {
            throw new NotImplementedException();
        }
    }
}
