namespace ERP
{
    public interface IAppFolders
    {
        string SampleProfileImagesFolder { get; }

        string WebLogsFolder { get; set; }

        string TempFileDownloadFolder { get; }

        string AttachmentsFolder { get; }

        string TempFileUploadFolder { get; }
    }
}