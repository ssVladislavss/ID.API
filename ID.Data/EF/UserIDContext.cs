using ID.Core.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ID.Data.EF
{
    public class UserIDContext : IdentityDbContext<UserID>
    {
        public UserIDContext(DbContextOptions<UserIDContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("user_store");

            base.OnModelCreating(builder);
        }
    }
}
