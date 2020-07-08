using ERP.Dto;
using ERP.Issue.Dto;
using ERP.Sprint.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Sprint.Exporting
{
   public interface ISprintListExcelExporter
    {
        FileDto ExportToFile(List<SprintListDto> list);
    }
}
