using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using ERP.Document.Dto;
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
        private readonly IAppFolders _appFolders;
        public DocumentAppService(IRepository<Models.Document,long> documentRepository,
            IAppFolders appFolders)
        {
            _documentRepository = documentRepository;
            _appFolders= appFolders;
        }
        public async Task<long> Create(CreateDocumentDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var document = ObjectMapper.Map<Models.Document>(input);
            if(document.DocumentName!= null)
            {
                // delete file document cu
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.AttachmentsFolder, document.DocumentName);
                var sourceFile = Path.Combine(_appFolders.TempFileUploadFolder, input.DocumentName);
                var destFile = Path.Combine(_appFolders.AttachmentsFolder, input.DocumentName);
                System.IO.File.Move(sourceFile, destFile);
                document.DocumentUrl = destFile;
                document.UploadDate = DateTime.Now;
                document.Size = input.Size;
            }
            await _documentRepository.InsertAsync(document);
            await CurrentUnitOfWork.SaveChangesAsync();
            return document.Id;
        }

        public async Task Delete(long id)
        {
            var doc = await _documentRepository.GetAsync(id);
            AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.AttachmentsFolder, doc.DocumentName);
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
            if(input.DocumentName !=null && input.IsSelectFile)
            {
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.AttachmentsFolder, input.DocumentName);
                var sourceFile = Path.Combine(_appFolders.TempFileUploadFolder, input.DocumentName);
                var destFile = Path.Combine(_appFolders.AttachmentsFolder, input.DocumentName);
                System.IO.File.Move(sourceFile, destFile);
                document.DocumentName = input.DocumentName;
                document.DocumentUrl = destFile;
                document.Size = input.Size;
            }
            document.Project_Id = input.Id;
            document.Id = input.Id;
            document.Discription = input.Discription;
            document.UploadDate = DateTime.Now;
            await _documentRepository.UpdateAsync(document);
        }

        public void DeleteDocumentTempFile(string DocumentName)
        {
            AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileUploadFolder, DocumentName);
        }
        public FileDto DownloadTempAttachment(long id)
        {
            var file = _documentRepository.Get(id);

            if (file != null && !string.IsNullOrEmpty(file.DocumentUrl) && File.Exists(file.DocumentUrl))
            {
                var zipFileDto = new FileDto(file.DocumentName, file.ContentType);

                var outputZipFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, zipFileDto.FileToken);

                File.Copy(file.DocumentUrl, outputZipFilePath, true);

                return zipFileDto;
            }
            return null;
        }
        //public async Task<FileDto> GetDocumentToExcel(int projectId)
        //{
        //    var projectDocuments = await _documentRepository.GetAll().Where(x => x.ProjectId == projectId).ToListAsync();
        //    return _documentExcelExporter.ExportToFile(ObjectMapper.Map<List<DocumentListDto>>(projectDocuments));
        //}

    }
}
