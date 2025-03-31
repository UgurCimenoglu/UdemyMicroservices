using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace FreeCourse.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);
            builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                .AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json")
                .AddEnvironmentVariables();
            });

            builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
            {
                options.Authority = builder.Configuration["IdentityServerUrl"];
                options.Audience = builder.Configuration["Audience"];
                options.RequireHttpsMetadata = false;
            });

            builder.Services.AddOcelot();

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.UseOcelot().Wait();
            app.Run();
        }
    }
}
