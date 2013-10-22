using System.ComponentModel.DataAnnotations;

namespace MvcLogin
{
    public class MemberLoginModel
    {
        [ScaffoldColumn(false)]
        public string ReturnUrl { get; set; }

        [Required, Display(Name = "Enter your username")]
        public string Username { get; set; }

        [Required, Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}