using Microsoft.Extensions.Configuration;
using MyShop.Core.Configurations;
using MyShop.Core.Interfaces.Managers;
using MyShop.Core.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MyShop.Core.Managers
{
    public class SmsSenderManager : ISmsSenderManager
    {
        IConfiguration _configuration;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twilioPhone;

        public SmsSenderManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _accountSid = _configuration["Twilio:AccountSid"];
            _authToken = _configuration["Twilio:AuthToken"];
            _twilioPhone = _configuration["Twilio:PhoneNumber"];
        }

        public void SendSms(IdentityMessageModel messageModel)
        {
            try
            {
                TwilioClient.Init(_accountSid, _authToken);

                var message = MessageResource.Create(body: messageModel.Content,
                                                     from: new Twilio.Types.PhoneNumber(_twilioPhone),
                                                     to: new Twilio.Types.PhoneNumber(messageModel.Destinations?[0])
                );
            }
            catch
            {
                Log.Current.Error("Error sending sms");
                throw;
            }
        }
    }
}
