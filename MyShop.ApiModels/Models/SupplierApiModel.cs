using MyShop.ApiModels.Models.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models
{
    public abstract class SupplierBaseApiModel : BaseApiModel<string>
    {
        [JsonProperty("title"), Required]              public string Title { get; set; }
        [JsonProperty("description")]                  public string Description { get; set; }
        [JsonProperty("email"), Required]              public string Email { get; set; }
        [JsonProperty("extra_email")]                  public string EmailExtra { get; set; }
        [JsonProperty("phone"), Required]              public string PhoneNumber { get; set; }
        [JsonProperty("extra_phone")]                  public string PhoneNumberExtra { get; set; }
        [JsonProperty("manager"), Required]            public string Manager { get; set; }
        [JsonProperty("extra_manager")]                public string ManagerExtra { get; set; }
        [JsonProperty("address"), Required]            public string Address { get; set; }
        [JsonProperty("extra_address")]                public string AddressExtra { get; set; }
        [JsonProperty("some_info")]                    public string SomeInfo { get; set; }
        [JsonProperty("language_id"), Required]        public int LanguageId { get; set; }
    }

    public class SupplierGetFullApiModel : SupplierBaseApiModel
    {
        [JsonProperty("products")]                     public List<ProductGetMinApiModel> Products { get; set; }
    }

    public class SupplierGetMinApiModel : SupplierBaseApiModel
    {
    }

    public class SupplierAddApiModel : SupplierBaseApiModel
    {
    }
}
