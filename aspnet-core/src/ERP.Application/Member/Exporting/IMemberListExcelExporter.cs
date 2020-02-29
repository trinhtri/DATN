using ERP.Dto;
using ERP.Member.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Member.Exporting
{
    public interface IMemberListExcelExporter
    {
        FileDto ExportToFile(List<MemberListDto> list);
    }
}
