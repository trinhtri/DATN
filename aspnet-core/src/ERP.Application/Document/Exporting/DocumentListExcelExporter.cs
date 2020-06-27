using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ERP.DataExporting.Excel.EpPlus;
using ERP.Document.Dto;
using ERP.Dto;
using ERP.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Document.Exporting
{
    public class DocumentListExcelExporter : EpPlusExcelExporterBase, IDocummentListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DocumentListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<DocumentListDto> list)
        {
            return CreateExcelPackage(
                "Documents.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Documments"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("DocumentName"),
                        L("Discription"),
                        L("UploadDate"),
                        L("DocumentUrl")
                    );

                    AddObjects(
                        sheet, 2, list,
                        _ => _.DocumentName,
                        _ => _.Discription,
                        _ => _timeZoneConverter.Convert(_.UploadDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.DocumentUrl
                        );

                //Formatting cells

                var timeColumn = sheet.Column(3);
                    timeColumn.Style.Numberformat.Format = "dd-mm-yyyy";

                    for (var i = 1; i <= 4; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
