using Abp.Dependency;

namespace ERP
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string SampleProfileImagesFolder { get; set; }

        public string WebLogsFolder { get; set; }

        public string TempFileDownloadFolder { get; set; } 

        public string AttachmentsFolder { get; set; }

        public string TempFileUploadFolder { get; set; }
    }
}