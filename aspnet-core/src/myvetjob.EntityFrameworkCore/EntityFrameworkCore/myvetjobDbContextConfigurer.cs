using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace myvetjob.EntityFrameworkCore
{
    public static class myvetjobDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<myvetjobDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<myvetjobDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}
