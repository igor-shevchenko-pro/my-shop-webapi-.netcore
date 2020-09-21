using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Validations.CustomAttributes
{
    public class FileEntityExtensionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string extension = value.ToString().ToLower();
                if (extension == "jpeg" || extension == "jpg" || extension == "png" || extension == "pdf")
                    return true;
                else
                    this.ErrorMessage = "FileEntity extension must be jpeg, jpg, png or pdf";
            }
            return false;
        }
    }

    public class FileEntitySizeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                byte[] file = value as byte[];

                if (file.Length / 1024 <= 5000)
                    return true;
                else
                    this.ErrorMessage = "FileEntity size can't be more than 5MB";
            }
            return false;
        }
    }
}
