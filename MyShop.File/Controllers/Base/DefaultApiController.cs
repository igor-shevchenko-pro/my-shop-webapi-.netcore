using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.ApiModels;
using MyShop.ApiModels.Interfaces.Base;
using MyShop.ApiModels.Models.Request;
using MyShop.ApiModels.Models.Response;
using MyShop.Core.Entities.Base;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Services.Base;

namespace MyShop.File.Controllers.Base
{
    [ApiController]
    public abstract class DefaultApiController<TModelAdd, TModelGetFull, TModelGetMin, TEntity, TKey, TSorting> : BaseApiController
                where TEntity : BaseEntity<TKey>
                where TModelAdd : IBaseApiModel<TKey>
                where TModelGetFull : IBaseApiModel<TKey>
                where TModelGetMin : IBaseApiModel<TKey>
                where TSorting : Enum
    {

        protected IBaseService<TModelAdd, TModelGetFull, TModelGetMin, TEntity, TKey, TSorting> _service;
        protected IDataMapper _dataMapper;

        public DefaultApiController(IBaseService<TModelAdd, TModelGetFull, TModelGetMin, TEntity, TKey, TSorting> service, IDataMapper dataMapper)
        {
            _service = service;
            _dataMapper = dataMapper;
        }

        [HttpGet("get/{id}")]
        public virtual async Task<ActionResult<IBaseApiModel<TKey>>> Get(TKey id, TypeModelResponseEnum modelResponseType)
        {
            var model = await _service.GetAsync(id, modelResponseType);

            return SuccessResult(model);
        }

        [HttpPost("get_all")]
        public virtual async Task<ActionResult<PaginationResponseApiModel<IBaseApiModel<TKey>, TSorting>>> Get(SortedEntitiesRequestApiModel<TSorting> model)
        {
            var models = await _service.GetAsync(model.Start, model.Count, model.Sortings, model.ModelResponseType, null, model.Query);
            return SuccessResult(models);
        }

        [HttpPost("get_all_by_user")]
        public virtual async Task<ActionResult<PaginationResponseApiModel<IBaseApiModel<TKey>, TSorting>>> GetByUser(SortedEntitiesByUserRequestApiModel<TSorting> model)
        {
            var userId = model.UserId ?? GetUserId();
            var models = await _service.GetAsync(userId, model.Start, model.Count, model.Sortings, model.ModelResponseType, null, model.Query);

            return SuccessResult(models);
        }


        [HttpPost("add")]
        public virtual async Task<ActionResult<SuccessResponseApiModel>> Add([FromBody] TModelAdd model)
        {
            TKey id = await _service.AddAsync(model);
            return SuccessResult(new SuccessResponseApiModel() { Response = "success", Id = id.ToString() });
        }

        [HttpPut("update/{id}")]
        public virtual async Task<ActionResult<string>> Update(TKey id, [FromBody] TModelAdd model)
        {
            await _service.UpdateAsync(model);
            return SuccessResult("success");
        }


        [HttpDelete("delete/{id}")]
        public virtual async Task<ActionResult<SuccessResponseApiModel>> Delete(TKey id)
        {
            await _service.DeleteAsync(id);
            return SuccessResult(new SuccessResponseApiModel() { Response = "success" });
        }


        [HttpPut("status/{id}")]
        [Authorize(Roles = "Admin, Super-Admin")]
        public virtual async Task<ActionResult<SuccessResponseApiModel>> ChangeActivityStatus(TKey id, [FromBody] EntityActivityStatusEnum activityStatus)
        {
            await _service.ChangeActivityStatusAsync(id, activityStatus);
            return SuccessResult(new SuccessResponseApiModel() { Response = "success" });
        }
    }
}