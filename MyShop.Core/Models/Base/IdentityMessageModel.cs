using System.Collections.Generic;

namespace MyShop.Core.Models
{
    public class IdentityMessageModel
    {
        public List<string> Destinations { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
