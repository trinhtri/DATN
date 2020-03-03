using Abp.Web.Models;


namespace ERP.Document.Dto
{
    public class UploadDocumentOutput : ErrorInfo
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public decimal FileSize { get; set; }

        public UploadDocumentOutput()
        {

        }

        public UploadDocumentOutput(ErrorInfo error)
        {
            Code = error.Code;
            Details = error.Details;
            Message = error.Message;
            ValidationErrors = error.ValidationErrors;
        }
    }
}
