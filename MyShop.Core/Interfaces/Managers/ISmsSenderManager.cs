using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Managers
{
    public interface ISmsSenderManager
    {
        void SendSms(IdentityMessageModel messageModel);
    }
}