using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ERP.DataExporting.Excel.EpPlus;
using ERP.Dto;
using ERP.Project.Dto;
using ERP.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Project.Exporting
{
    public class ProjectListExcelExporter : EpPlusExcelExporterBase, IProjectListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ProjectListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<ProjectListDto> list)
        {
            return CreateExcelPackage(
                "Projects.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Projects"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ProjectCode"),
                        L("ProjectName"),
                        L("StartDate"),
                        L("EndDate"),
                        L("ClientName"),
                        L("Status"),
                        L("Note")
                    );

                    AddObjects(
                        sheet, 2, list,
                        _ => _.ProjectCode,
                        _ => _.ProjectName,
                        _ => _timeZoneConverter.Convert(_.StartDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.EndDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.ClientName,
                        _ => _.Status ==true ? L("Active") : L("Compeleted"),
                        _ => _.Note
                        );

                    //Formatting cells

                    var timeColumn = sheet.Column(3);
                    timeColumn.Style.Numberformat.Format = "dd-mm-yyyy";

                    var timeColumn1 = sheet.Column(4);
                    timeColumn1.Style.Numberformat.Format = "dd-mm-yyyy";

                    for (var i = 1; i <= 6; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
