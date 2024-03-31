using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using myvetjob.Authorization.Roles;
using myvetjob.Authorization.Users;
using myvetjob.MultiTenancy;

namespace myvetjob.EntityFrameworkCore
{
    public class myvetjobDbContext : AbpZeroDbContext<Tenant, Role, User, myvetjobDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public myvetjobDbContext(DbContextOptions<myvetjobDbContext> options)
            : base(options)
        {
        }
    }
}
