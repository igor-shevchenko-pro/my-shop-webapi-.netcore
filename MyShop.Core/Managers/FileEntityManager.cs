using MyShop.Core.Helpers;
using MyShop.Core.Interfaces.Managers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyShop.Core.Managers
{
    public class FileEntityManager : IFileEntityManager
    {
        private readonly object locker = new object();

        public async Task<byte[]> Read(string fileEntityId, string fileEntityExtension)
        {
            var path = GetPath(fileEntityId, fileEntityExtension);
            if (!File.Exists(path))
            {
                throw new ArgumentException($"Path is not found");
            }

            var bytes = await File.ReadAllBytesAsync(path);
            return bytes;
        }

        public Task Delete(string fileEntityId, string fileEntityExtension)
        {
            return Task.Run(() =>
            {
                var path = GetPath(fileEntityId, fileEntityExtension);
                if (File.Exists(path))
                {
                    lock (locker)
                    {
                        File.Delete(path);
                    }
                }
            });
        }

        public async Task Save(string fileEntityId, string fileEntityExtension, byte[] bytes)
        {
            var path = GetPath(fileEntityId, fileEntityExtension);
            await File.WriteAllBytesAsync(path, bytes);
        }

        protected string GetPath(string fileEntityId, string fileEntityExtension)
        {
            if (string.IsNullOrEmpty(fileEntityId)) throw new ArgumentException("FileEntityId is empty");

            var isDefaultFile = FileEntityHelper.Current.IsDefaultFile(fileEntityId);
            if (fileEntityId.Length < 36 && !isDefaultFile) throw new ArgumentException($"FileEntityId is invalid");

            string fileEntityType = null;
            string root = null;
            string pathToFile = null;
            string fileName = $"{fileEntityId}.{fileEntityExtension}";

            if (isDefaultFile)
            {
                fileEntityType = "Default";
                root = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileEntityType);
                pathToFile = Path.Combine(root);
            }
            else
            {
                fileEntityType = "Images";
                root = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileEntityType);
                pathToFile = Path.Combine(root, fileEntityId[0].ToString(), fileEntityId[1].ToString(), fileEntityId[2].ToString());
            }

            CheckFolder(pathToFile);

            return Path.Combine(pathToFile, fileName);
        }

        protected bool CheckFolder(string path, bool create = true)
        {
            var isExists = Directory.Exists(path);

            if (create && !isExists)
            {
                Directory.CreateDirectory(path);
                isExists = true;
            }

            return isExists;
        }
    }
}
