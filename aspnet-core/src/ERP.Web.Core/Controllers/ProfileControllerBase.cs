using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using ERP.Authorization.Users.Profile.Dto;
using ERP.Document.Dto;
using ERP.Dto;
using ERP.Storage;
using ERP.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Controllers
{
    public abstract class ProfileControllerBase : ERPControllerBase
    {
        private readonly IAppFolders _appFolders;

        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const int MaxProfilePictureSize = 5242880; //5MB

        protected ProfileControllerBase(ITempFileCacheManager tempFileCacheManager, IAppFolders appFolders)
        {
            _tempFileCacheManager = tempFileCacheManager;
            _appFolders = appFolders;
        }

        public UploadProfilePictureOutput UploadProfilePicture(FileDto input)
        {
            try
            {
                var profilePictureFile = Request.Form.Files.First();

                //Check input
                if (profilePictureFile == null)
                {
                    throw new UserFriendlyException(L("Documnet_Change_Error"));
                }

                if (profilePictureFile.Length > MaxProfilePictureSize)
                {
                    throw new UserFriendlyException(L("Document_Warn_SizeLimit", AppConsts.MaxProfilPictureBytesUserFriendlyValue));
                }

                byte[] fileBytes;
                using (var stream = profilePictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception(L("IncorrectImageFormat"));
                }

                _tempFileCacheManager.SetFile(input.FileToken, fileBytes);

                using (var bmpImage = new Bitmap(new MemoryStream(fileBytes)))
                {
                    return new UploadProfilePictureOutput
                    {
                        FileToken = input.FileToken,
                        FileName = input.FileName,
                        FileType = input.FileType,
                        Width = bmpImage.Width,
                        Height = bmpImage.Height
                    };
                }
            }
            catch (UserFriendlyException ex)
            {
                return new UploadProfilePictureOutput(new ErrorInfo(ex.Message));
            }
        }

        public UploadDocumentOutput UploadDocument()
        {
            try
            {
                var document = Request.Form.Files.First();

                //Check input
                if (document == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                if (document.Length > MaxProfilePictureSize)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit", AppConsts.MaxProfilPictureBytesUserFriendlyValue));
                }

                byte[] fileBytes;
                using (var stream = document.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }
                var fileInfo = new FileInfo(document.FileName);
                var tempFilePath = Path.Combine(_appFolders.TempFileUploadFolder, document.FileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                return new UploadDocumentOutput()
                {
                    ContentType = document.ContentType,
                    FileName = document.FileName,
                    FileSize = Math.Round((decimal)document.Length / 1048576, 2)
                };

            }
            catch (UserFriendlyException ex)
            {
                return new UploadDocumentOutput(new ErrorInfo(ex.Message));
            }
        }
        public ActionResult DownloadTempFile(FileDto file)
        {
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            if (!System.IO.File.Exists(filePath))
            {
                throw new UserFriendlyException(L("RequestedFileDoesNotExists"));
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(fileBytes, file.FileType, file.FileName);
        }
    }
}