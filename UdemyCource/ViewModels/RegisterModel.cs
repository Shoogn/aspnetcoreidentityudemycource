using System.ComponentModel.DataAnnotations;

namespace UdemyCource.ViewModels
{
    public class RegisterModel
    {
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserAge { get; set; }
    }
}
