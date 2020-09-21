using Microsoft.AspNetCore.Http;
using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.Core.Entities;
using MyShop.Core.Managers.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Core.Helpers
{
    public class FileEntityHelper
    {
        private static readonly FileEntityHelper _instance = new FileEntityHelper();
        public static FileEntityHelper Current => _instance;

        public FileEntity DefaultUser { get; private set; }
        public FileEntity DefaultCustomer { get; private set; }
        public FileEntity Border { get; private set; }
        public FileEntity BorderLogo { get; private set; }
        public FileEntity MainLogo { get; private set; }
        public List<FileEntity> Collection { get; private set; }
        public List<FileEntity> FullCollection { get; private set; }

        private readonly string _permitedUploadFileEntitySize;
        private readonly string _permitedUploadFileEntityExtensions;

        private FileEntityHelper()
        {
            Initialize();

            FullCollection = new List<FileEntity>();
            FullCollection.AddRange(InitializeFiles());

            _permitedUploadFileEntitySize = ConfigurationBuilderManager.Current.GetConfiguration("FileEntityValidationPermitions:PermitedUploadFileEntitySize");
            _permitedUploadFileEntityExtensions = ConfigurationBuilderManager.Current.GetConfiguration("FileEntityValidationPermitions:PermitedUploadFileEntityExtensions");
        }

        public bool IsDefaultFile(string id)
        {
            var file = Collection.FirstOrDefault(x => x.Id == id);
            return (file == null) ? false : true;
        }

        public async Task<byte[]> ConvertIFormFileToByteArray(IFormFile formFile)
        {
            byte[] bytes = null;
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }

        public FileEntityAddApiModel BuildFileAddApiModel(string originalName, string extension, string type, byte[] bytes,
            string imageGalleryId = null)
        {
            FileEntityTypeEnum fileType = GetFileType(type);
            return new FileEntityAddApiModel()
            {
                OriginalName = originalName,
                Extension = extension,
                Type = fileType,
                Bytes = bytes,
                ImageGalleryId = imageGalleryId
            };
        }

        public void ValidateFileSize(byte[] bytes)
        {
            if (bytes.Length > Convert.ToInt32(_permitedUploadFileEntitySize))
            {
                throw new Exception("FileEntity size can't be more than 5MB");
            }
        }

        public void ValidateFileExtension(string extension)
        {
            var extensions = _permitedUploadFileEntityExtensions.Split(',');
            if (!extensions.Contains(extension)) throw new Exception("FileEntity extension must be jpeg, jpg, png or pdf");
        }


        private IEnumerable<FileEntity> InitializeFiles()
        {
            return Collection;
        }

        private void Initialize()
        {
            DefaultUser = new FileEntity()
            {
                Id = "default-user",
                OriginalName = "default-user",
                Extension = "jpg",
                Type = FileEntityTypeEnum.AvatarImage
            };

            DefaultCustomer = new FileEntity()
            {
                Id = "default-customer",
                OriginalName = "default-customer",
                Extension = "jpg",
                Type = FileEntityTypeEnum.AvatarImage
            };

            Border = new FileEntity()
            {
                Id = "border",
                OriginalName = "border",
                Extension = "png",
                Type = FileEntityTypeEnum.Image
            };

            BorderLogo = new FileEntity()
            {
                Id = "border-logo",
                OriginalName = "border-logo",
                Extension = "png",
                Type = FileEntityTypeEnum.Image
            };

            MainLogo = new FileEntity()
            {
                Id = "main-logo",
                OriginalName = "main-logo",
                Extension = "png",
                Type = FileEntityTypeEnum.Image
            };

            Collection = new List<FileEntity>()
            {
                DefaultUser,
                DefaultCustomer,
                Border,
                BorderLogo,
                MainLogo,
            };
        }

        private FileEntityTypeEnum GetFileType(string type)
        {
            switch (type)
            {
                case "0":
                    return FileEntityTypeEnum.IconImage;
                case "1":
                    return FileEntityTypeEnum.AvatarImage;
                case "2":
                    return FileEntityTypeEnum.Image;
                case "3":
                    return FileEntityTypeEnum.Video;
                case "4":
                    return FileEntityTypeEnum.Document;
                case "5":
                    return FileEntityTypeEnum.Unknown;

                default:
                    return FileEntityTypeEnum.Image;
            }
        }
    }
}
