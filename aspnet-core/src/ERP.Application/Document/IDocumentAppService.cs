using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.Document.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Document
{
    public interface IDocumentAppService: IApplicationService
    {
        Task<PagedResultDto<DocumentListDto>> GetAll(DocumentInputDto input);
        Task<long> Create(CreateDocumentDto input);
        Task Delete(long id);
        Task Update(CreateDocumentDto input);
        Task<CreateDocumentDto> GetId(long id);
    }
}
