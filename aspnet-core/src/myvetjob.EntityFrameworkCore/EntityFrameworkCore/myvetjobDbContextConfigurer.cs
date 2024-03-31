using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace myvetjob.EntityFrameworkCore
{
    public static class myvetjobDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<myvetjobDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<myvetjobDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
