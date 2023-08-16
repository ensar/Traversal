using System.ComponentModel.DataAnnotations;

namespace Traversal.Models
{
    public class UserSignInVM
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adınızı giriniz.")]
        public string username { get; set; }
        [Required(ErrorMessage = "Lütfen şifrenizi adınızı giriniz.")]
        public string password { get; set; }
    }

}
