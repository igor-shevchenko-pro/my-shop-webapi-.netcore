using MyShop.ApiModels.Models.Base;

namespace MyShop.ApiModels.Models
{
    public abstract class ProductBaseApiModel : BaseApiModel<string>
    {
    }

    public class ProductGetFullApiModel : ProductBaseApiModel
    {
    }

    public class ProductGetMinApiModel : ProductBaseApiModel
    {
    }

    public class ProductAddApiModel : ProductBaseApiModel
    {
    }
}
