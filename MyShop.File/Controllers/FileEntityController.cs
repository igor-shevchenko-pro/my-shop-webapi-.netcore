using System.Threading.Tasks;
using MyShop.ApiModels.Models;
using MyShop.Core.Interfaces.Services;
using MyShop.File.Controllers.Base;
using MyShop.ApiModels.Models.Response;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace MyShop.File.Controllers
{
    [Authorize]
    [Route("api/File")]
    public class FileEntityController : DefaultApiController<FileEntityAddApiModel, FileEntityGetFullApiModel, FileEntityGetMinApiModel, FileEntity, string, FileEntitySortingEnum>
    {
        private readonly new IFileEntityService _service;

        public FileEntityController(IFileEntityService service, IDataMapper dataMapper) 
            : base(service, dataMapper)
        {
            _service = service;
        }

        [HttpPost("add")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<SuccessResponseApiModel>> Add()
        {
            // get file from request
            var file = Request.Form.Files[0];

            // get file params
            var fileExtension = (file.ContentType.Split('/')[1]).ToLower();
            var fileName = file.Name;

            // parse fileType and imageGalleryId
            var dataArray = file.FileName.Split(';');
            var type = dataArray[0];

            string imageGalleryId = null;
            if (dataArray.Length > 1) imageGalleryId = dataArray[1];

            byte[] bytes = await FileEntityHelper.Current.ConvertIFormFileToByteArray(file);

            // validate file
            FileEntityHelper.Current.ValidateFileExtension(fileExtension);
            FileEntityHelper.Current.ValidateFileSize(bytes);

            // build fileAddApiModel
            var model = FileEntityHelper.Current.BuildFileAddApiModel(fileName, fileExtension, type, bytes, imageGalleryId);

            // save file in store
            var result = await _service.AddAsync(model);
            return SuccessResult(new SuccessResponseApiModel() { Response = "success", Id = result });
        }

        [AllowAnonymous]
        [HttpGet("get_data/{id}")]
        public async Task<ActionResult> GetData(string id)
        {
            var model = await _service.GetDataAsync(id);

            var response = File(model.Bytes, model.MimeType);  //var response = FileEntity(model.Bytes, "application/octet-stream");
            response.FileDownloadName = model.OriginalName + "." + model.Extension;

            return response;
        }

        [HttpPost("delete_range")]
        public virtual async Task<ActionResult<SuccessResponseApiModel>> DeleteRange([FromBody] List<string> ids)
        {
            await _service.DeleteRange(ids);
            return SuccessResult(new SuccessResponseApiModel() { Response = "success" });
        }


        //[HttpPost("add_files")]
        //public async Task<ActionResult<List<SeccessResponseApiModel>>> AddFiles([FromBody] FileEntitiesAddApiModel models)
        //{
        //    var result = await _service.AddRange(models);

        //    var successResults = new List<SeccessResponseApiModel>();
        //    if(result != null && result.Count > 0)
        //    {
        //        foreach(var item in result)
        //        {
        //            successResults.AddAsync(new SeccessResponseApiModel() { Response = "success", Id = item.ToString() });
        //        }
        //    }

        //    return SuccessResult(successResults);
        //}


        #region NonAction
        [NonAction]
        public override Task<ActionResult<SuccessResponseApiModel>> Add([FromBody] FileEntityAddApiModel model)
        {
            throw new Exception();
        }

        [NonAction]
        public override Task<ActionResult<string>> Update(string id, [FromBody] FileEntityAddApiModel model)
        {
            throw new Exception();
        }
        #endregion
    }
}