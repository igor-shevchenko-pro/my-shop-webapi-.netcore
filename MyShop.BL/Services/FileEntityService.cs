using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.BL.Services.Base;
using MyShop.Core.Configurations;
using MyShop.Core.Entities;
using MyShop.Core.Helpers;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Managers;
using MyShop.Core.Interfaces.Managers.Base;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.BL.Services
{
    public class FileEntityService : BaseService<FileEntityAddApiModel, FileEntityGetFullApiModel, FileEntityGetMinApiModel, FileEntity, string, FileEntitySortingEnum>, IFileEntityService
    {
        protected IFileEntityManager _fileEntityManager;
        protected IFileEntityExtensionManager _fileEntityExtensionManager;

        public FileEntityService(IFileEntityRepository repository,
            IFileEntityManager fileEntityManager,
            IFileEntityExtensionManager fileEntityExtensionManager,
            IDataMapper dataMapper) : base(repository, dataMapper)
        {
            _fileEntityManager = fileEntityManager;
            _fileEntityExtensionManager = fileEntityExtensionManager;
        }

        public override async Task<string> AddAsync(FileEntityAddApiModel model)
        {
            var entity = _dataMapper.Parse<FileEntityAddApiModel, FileEntity>(model);
            await _repository.AddAsync(entity);

            await _fileEntityManager.Save(entity.Id, model.Extension, model.Bytes);
            return entity.Id;
        }

        public override async Task DeleteAsync(string id, int? langId = null)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            if(entity == null)
            {
                throw new Exception("Entity is not found");
            }

            var isDefaultFile = FileEntityHelper.Current.IsDefaultFile(id);
            if (!isDefaultFile)
            {
                await _fileEntityManager.Delete(id, entity.Extension);
                await _repository.DeleteAsync(id);
            }
        }

        public async Task DeleteRange(List<string> ids)
        {
            if(ids != null && ids.Count > 0)
            {
                foreach(var id in ids)
                {
                    await DeleteAsync(id);
                }
            }
        }

        public async Task<List<string>> AddRange(FileEntitiesAddApiModel model)
        {
            var result = new List<string>();
            foreach (var item in model.FileEntities)
            {
                item.ImageGalleryId = model.ImageGalleryId;

                try
                {
                    result.Add(await AddAsync(item));
                }
                catch (Exception ex)
                {
                    Log.Current.Error(ex);
                }
            }
            return result;
        }

        public async Task<FileEntityGetFullApiModel> GetDataAsync(string id)
        {
            var model = await base.GetAsync(id, TypeModelResponseEnum.GetFullApiModel) as FileEntityGetFullApiModel;

            if (model == null) throw new Exception("FileEntity isn't found");

            model.Bytes = await _fileEntityManager.Read(model.Id, model.Extension);
            model.MimeType = _fileEntityExtensionManager.GetMimeType(model.Extension);

            return model;
        }

        public async Task<IEnumerable<FileEntityGetMinApiModel>> GetRangeAsync(IEnumerable<string> fileEntityIds)
        {
            var entities = new List<FileEntity>();
            foreach (var item in fileEntityIds)
            {
                var entity = await _repository.GetAsync(item);
                entities.Add(entity);
            }

            var models = _dataMapper.ParseCollection<FileEntity, FileEntityGetMinApiModel>(entities);

            return models;
        }
    }
}
