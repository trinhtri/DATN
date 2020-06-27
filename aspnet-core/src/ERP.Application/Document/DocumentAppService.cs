using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using ERP.Document.Dto;
using ERP.Document.Exporting;
using ERP.Dto;
using ERP.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Document
{
    public class DocumentAppService : ERPAppServiceBase, IDocumentAppService
    {
        private readonly IRepository<Models.Document,long> _documentRepository;
        private DocumentListExcelExporter _docummentListExcelExporter;
        public DocumentAppService(IRepository<Models.Document,long> documentRepository, DocumentListExcelExporter docummentListExcelExporter)
        {
            _documentRepository = documentRepository;
            _docummentListExcelExporter = docummentListExcelExporter;
        }
        public async Task<long> Create(CreateDocumentDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var document = ObjectMapper.Map<Models.Document>(input);
            await _documentRepository.InsertAsync(document);
            await CurrentUnitOfWork.SaveChangesAsync();
            return document.Id;
        }

        public async Task Delete(long id)
        {
            await _documentRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<DocumentListDto>> GetAll(DocumentInputDto input)
        {
            var list = _documentRepository.GetAll().Where(x => x.Project_Id == input.Project_Id);
            var tatolCount = await list.CountAsync();
            var result = await list.OrderBy(input.Sorting)
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
            var document = await _documentRepository.FirstOrDefaultAsync(input.Id);
            ObjectMapper.Map(input, document);
        }

        public async Task<FileDto> GetDocumentToExcel(DocumentInputDto inputDto)
        {
            var list = await GetAll(inputDto);
            var dto = list.Items.ToList();
            return _docummentListExcelExporter.ExportToFile(dto);
        }
    }
}
