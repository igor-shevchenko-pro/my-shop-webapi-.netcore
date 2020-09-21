using MyShop.ApiModels.Models.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models
{
    public abstract class RoleBaseApiModel : BaseApiModel<string>
    {
        [JsonProperty("title"), Required]          public string Title { get; set; }
    }

    public class RoleGetFullApiModel : RoleBaseApiModel
    {
        [JsonProperty("users")]                    public List<UserGetMinApiModel> Users { get; set; }
    }

    public class RoleGetMinApiModel : RoleBaseApiModel
    {
    }

    public class RoleAddApiModel : RoleBaseApiModel
    {
    }
}