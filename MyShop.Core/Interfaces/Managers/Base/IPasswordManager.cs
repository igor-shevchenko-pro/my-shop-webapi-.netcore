using MyShop.ApiModels;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Managers.Base
{
    public interface IPasswordManager
    {
        Task ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task RecoveryPasswordRequestAsync(string contact, ContactTypeEnum contacType, FrontClientType frontClient);
        Task RecoveryPasswordAsync(string contact, string key, string newPassword, ContactTypeEnum contactType);
    }
}
