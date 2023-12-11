using System.ComponentModel.DataAnnotations;

namespace Mamba.ViewModels
{
    public class MemberLoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
