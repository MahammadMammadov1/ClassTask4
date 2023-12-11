using System.ComponentModel.DataAnnotations;

namespace Mamba.Areas.Manage.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
