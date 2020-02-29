using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ERP.Document.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Document
{
    public class DocumentAppService : ERPAppServiceBase, IDocumentAppService
    {
        private readonly IRepository<Models.Document> _documentRepository;
        public DocumentAppService(IRepository<Models.Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }
        public Task<int> Create(CreateDocumentDto input)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
           await _documentRepository.DeleteAsync(id);
        }

        public Task<PagedResultDto<DocumentListDto>> GetAll(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<CreateDocumentDto> GetId(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(CreateDocumentDto input)
        {
            throw new NotImplementedException();
        }
    }
}
