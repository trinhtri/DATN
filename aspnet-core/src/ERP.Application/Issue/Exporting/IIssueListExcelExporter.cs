using ERP.Dto;
using ERP.Issue.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Issue.Exporting
{
   public interface IIssueListExcelExporter
    {
        FileDto ExportIssueToFile(List<IssueListDto> list);
        //FileDto ExportSprintToFile(List<IssueListDto> list);
    }
}
