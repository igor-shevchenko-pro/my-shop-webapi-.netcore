namespace MyShop.Core.Models.Base
{
    public class EmailSettings
    {
        public string SenderName { get; set; }
        public string MailServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}