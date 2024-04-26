using System;
using Abp.AspNetCore.Dependency;
using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sentry.AspNetCore;
using Sentry.Extensibility;

namespace myvetjob.Web.Startup
{
    public class Program
    {
        private static readonly string SENTRY_DSN = Environment.GetEnvironmentVariable("SENTRY_DSN");
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSentry(o => {
                        o.Dsn = SENTRY_DSN;
                        // When configuring for the first time, to see what the SDK is doing:
                        o.Debug = false;
                        // Set TracesSampleRate to 1.0 to capture 100%
                        // of transactions for performance monitoring.
                        // We recommend adjusting this value in production
                        o.TracesSampleRate = 0.8;
                        // Sample rate for profiling, applied on top of othe TracesSampleRate,
                        // e.g. 0.2 means we want to profile 20 % of the captured transactions.
                        // We recommend adjusting this value in production.
                        o.ProfilesSampleRate = 0.8;
                        // Requires NuGet package: Sentry.Profiling
                        // Note: By default, the profiler is initialized asynchronously. This can
                        // be tuned by passing a desired initialization timeout to the constructor.
                    });
                })
                .UseCastleWindsor(IocManager.Instance.IocContainer);
    }
}
