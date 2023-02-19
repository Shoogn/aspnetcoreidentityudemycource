using Microsoft.AspNetCore.Identity;

namespace UdemyCource.Models
{
    public class AppUser : IdentityUser
    {
        public string UserAge { get; set; }
    }
}
