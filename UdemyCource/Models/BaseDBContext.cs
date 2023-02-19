using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UdemyCource.Models
{
    public class BaseDBContext : IdentityDbContext<AppUser>
    {
        public BaseDBContext(DbContextOptions<BaseDBContext> options) : base(options) { }
    }
}
