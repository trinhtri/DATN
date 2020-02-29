using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ERP.DataExporting.Excel.EpPlus;
using ERP.Dto;
using ERP.Member.Dto;
using ERP.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Member.Exporting
{
    public class MemberListExcelExporter : EpPlusExcelExporterBase, IMemberListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MemberListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<MemberListDto> list)
        {
            return CreateExcelPackage(
                "Members.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Members"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("EmployeeName"),
                        L("Role"),
                        L("EffectiveDate"),
                        L("EndDate"),
                        L("Note")
                    );

                    AddObjects(
                        sheet, 2, list,
                        _ => _.EmployeeName,
                        _ => _.Role,
                        _ => _timeZoneConverter.Convert(_.EffectiveDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.EndDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Note
                        );

                    //Formatting cells

                    var timeColumn = sheet.Column(3);
                    timeColumn.Style.Numberformat.Format = "dd-mm-yyyy";

                    var timeColumn1 = sheet.Column(4);
                    timeColumn1.Style.Numberformat.Format = "dd-mm-yyyy";

                    for (var i = 1; i <= 5; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
