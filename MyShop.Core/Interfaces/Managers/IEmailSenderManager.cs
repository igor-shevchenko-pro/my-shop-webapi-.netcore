using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Managers
{
    public interface IEmailSenderManager
    {
        void SendEmail(IdentityMessageModel messageModel);
    }
}