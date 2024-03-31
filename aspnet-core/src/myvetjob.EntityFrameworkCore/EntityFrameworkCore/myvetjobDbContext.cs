using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using myvetjob.Authorization.Roles;
using myvetjob.Authorization.Users;
using myvetjob.MultiTenancy;
using Abp.Localization;

namespace myvetjob.EntityFrameworkCore
{
    public class myvetjobDbContext : AbpZeroDbContext<Tenant, Role, User, myvetjobDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public myvetjobDbContext(DbContextOptions<myvetjobDbContext> options)
            : base(options)
        {
        }

        // add these lines to override max length of property
        // we should set max length smaller than the PostgreSQL allowed size (10485760)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationLanguageText>()
                .Property(p => p.Value)
                .HasMaxLength(100); // any integer that is smaller than 10485760
        }
    }
}
