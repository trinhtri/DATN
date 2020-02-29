using ERP.Dto;
using ERP.Project.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Project.Exporting
{
   public interface IProjectListExcelExporter
    {
        FileDto ExportToFile(List<ProjectListDto> list);
    }
}
