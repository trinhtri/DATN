using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ERP.DataExporting.Excel.EpPlus;
using ERP.Dto;
using ERP.Issue.Dto;
using ERP.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Issue.Exporting
{
    public class IssueListExcelExporter : EpPlusExcelExporterBase,IIssueListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public IssueListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<IssueListDto> list)
        {
            return CreateExcelPackage(
                "Issues.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Issues"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("IssueCode"),
                        L("IssueName"),
                        L("Type"),
                        L("Status"),
                        L("Discription"),
                        L("Project_Id"),
                        L("Assignee_Id"),
                        L("Reporter_Id"),
                        L("Due_Date"),
                        L("Update_Date"),
                        L("Resolved_Date"),
                        L("Estimate")
                    );

                    AddObjects(
                        sheet, 2, list,
                        _ => _.IssueCode,
                        _ => _.IssueName,
                        _=>_.Type,
                        _=>_.Status,
                        _=>_.Discription,
                        _=>_.Project_Id,
                        _=>_.Assignee_Id,
                        _=>_.Reporter_Id,
                        _ => _timeZoneConverter.Convert(_.Due_Date, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Update_Date, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Resolved_Date, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Estimate
                        );

                    //Formatting cells

                    var timeColumn = sheet.Column(9);
                    timeColumn.Style.Numberformat.Format = "dd-mm-yyyy";

                    var timeColumn1 = sheet.Column(10);
                    timeColumn1.Style.Numberformat.Format = "dd-mm-yyyy";

                    var timeColumn2 = sheet.Column(11);
                    timeColumn2.Style.Numberformat.Format = "dd-mm-yyyy";

                    for (var i = 1; i <= 12; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}