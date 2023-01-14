using System.ComponentModel.DataAnnotations;

namespace Mekashron.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public int CountryID { get; set; }
        public int aID { get; set; }
        public string SignupIP { get; set; }
    }
}
