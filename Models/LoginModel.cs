using System.ComponentModel.DataAnnotations;

namespace MonNgonMoiNgay.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberLogin { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
