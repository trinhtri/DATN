using Abp.Application.Services;
using ERP.Document.Dto;
using ERP.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Document.Exporting
{
   public interface IDocummentListExcelExporter:IApplicationService
    {
        FileDto ExportToFile(List<DocumentListDto> list);

    }
}
