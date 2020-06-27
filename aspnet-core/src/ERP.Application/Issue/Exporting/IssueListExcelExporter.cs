using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ERP.Authorization.Users;
using ERP.DataExporting.Excel.EpPlus;
using ERP.Dto;
using ERP.Issue.Dto;
using ERP.Models;
using ERP.Storage;
using System.Collections.Generic;

namespace ERP.Issue.Exporting
{
    public class IssueListExcelExporter : EpPlusExcelExporterBase,IIssueListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Models.Issue, long> _issueRepository;
        private readonly IRepository<Models.Project, long> _projectRepository;

        public IssueListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            IRepository<User, long> userRepository,
            IRepository<Models.Issue, long> issueRepository,
            IRepository<Models.Project, long> projectRepository,
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _issueRepository = issueRepository;
        }

        public FileDto ExportIssueToFile(List<IssueListDto> list)
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
                        L("Sprint"),
                        L("Assignee"),
                        L("Reporter"),
                        L("Due_Date"),
                        L("Estimate")
                    );

                    AddObjects(
                        sheet, 2, list,
                        _ => _.IssueCode,
                        _ => _.Summary,
                        _=> GetTypeName(_.Type_Id),
                        _=> GetStatusName(_.Status_Id),
                        _=>_.Discription,
                        _=>GetNameSprint(_.Parent_Id ?? 1),
                        _=> GetName(_.Assignee_Id),
                        _=> GetName(_.Reporter_Id),
                        _ => _timeZoneConverter.Convert(_.Due_Date, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Estimate
                        );

                    var timeColumn = sheet.Column(9);
                    timeColumn.Style.Numberformat.Format = "dd-mm-yyyy";

                    for (var i = 1; i <= 10; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

        public FileDto ExportSprintToFile(List<IssueListDto> list)
        {
            return CreateExcelPackage(
                 "Issues.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("Issues"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         L("SprintCode"),
                         L("SprintName"),
                         L("Type"),
                         L("Status"),
                         L("Discription"),
                         L("Project"),
                         L("Assignee"),
                         L("Reporter"),
                         L("Due_Date"),
                         L("Estimate")
                     );

                     AddObjects(
                         sheet, 2, list,
                         _ => _.IssueCode,
                         _ => _.Summary,
                         _ => GetTypeName(_.Type_Id),
                         _ => GetStatusName(_.Status_Id),
                         _ => _.Discription,
                         _ => GetNameProject(_.Project_Id),
                         _ => GetName(_.Assignee_Id),
                         _ => GetName(_.Reporter_Id),
                         _ => _timeZoneConverter.Convert(_.Due_Date, _abpSession.TenantId, _abpSession.GetUserId()),
                         _ => _.Estimate
                         );

                     var timeColumn = sheet.Column(9);
                     timeColumn.Style.Numberformat.Format = "dd-mm-yyyy";

                     for (var i = 1; i <= 10; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }


        private string GetName(long id)
        {
            var user = _userRepository.FirstOrDefaultAsync(id);
            return user.Result.UserName;
        }
        private string GetTypeName(long type)
        {
            var result = "";
            switch (type)
            {
                case 1:
                    result= L("NewFeature");
                    break;
                case 2:
                    result = L("Improvement");
                    break;
                case 3:
                    result = L("Bug");
                    break;
            }
            return result;
        }

        private string GetStatusName(long id)
        {
            var result = "";
            switch (id)
            {
                case 1:
                    result = this.L("Open");
                    break;
                case 2:
                    result = this.L("InProgress");
                    break;
                case 3:
                    result = this.L("Resolved");
                    break;
                case 4:
                    result = this.L("Compeleted");
                    break;
                case 5:
                    result = this.L("ReOpened");
                    break;
            }
            return result;
        }

        private string GetNameProject(long? id)
        {
            var project = _projectRepository.FirstOrDefault(id ?? 1);
            return project.ProjectName;
        }
        private string GetNameSprint(long id)
        {
            var sprint = _issueRepository.FirstOrDefault(id);
            return sprint.IssueCode;
        }

        
    }
}