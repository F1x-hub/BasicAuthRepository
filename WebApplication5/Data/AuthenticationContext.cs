using Microsoft.EntityFrameworkCore;
using WebApplication5.Model;

namespace WebApplication5.Data
{
    public class AuthenticationContext : DbContext
    {
        public DbSet<User> users { get; set; }

        public AuthenticationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
