using System.ComponentModel.DataAnnotations;

namespace Mekashron.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string IPs { get; set; }


    }
}
