using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MyShop.Core.Configurations;
using MyShop.Core.Interfaces.Managers;
using MyShop.Core.Models;
using MyShop.Core.Models.Base;
using System.Linq;

namespace MyShop.Core.Managers
{
    public class EmailSenderManager : IEmailSenderManager
    {
        private readonly EmailSettings _emailSettings;

        public EmailSenderManager(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(IdentityMessageModel messageModel)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    var mimeMessage = CreateMimeMessage(messageModel);

                    client.Connect(_emailSettings.MailServer, _emailSettings.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                    client.Send(mimeMessage);
                }
                catch
                {
                    Log.Current.Error("Error sending email");
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateMimeMessage(IdentityMessageModel messageModel)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Username));
            mimeMessage.To.AddRange(messageModel.Destinations.Select(x => new MailboxAddress(_emailSettings.SenderName, x)));
            mimeMessage.Subject = messageModel.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = messageModel.Content };

            return mimeMessage;
        }
    }
}