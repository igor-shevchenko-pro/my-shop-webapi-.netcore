using MyShop.Core.Entities.Base;
using MyShop.Core.Interfaces.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Core.Entities.UserAccount
{
    public class UserProfile : BaseEntity<string>, IBaseLanguageEntity<string>
    {
        [Key, ForeignKey("User")]
        public override string Id { get; set; }
        public virtual User User { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }


        [ForeignKey("FileEntity")]
        public string FileEntityId { get; set; }
        public virtual FileEntity FileEntity { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public int GenderId { get; set; }
        public int GenderLanguageId { get; set; }
        public virtual Gender Gender { get; set; }
    }
}
