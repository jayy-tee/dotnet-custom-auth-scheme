using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using WebApp.ServiceInterface;

namespace WebApp
{
    public class Startup : ModularStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public new void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("WebApp", typeof(MyServices).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            // Plugins.Add(new AuthFeature(() => new AuthUserSession(),
            //     new IAuthProvider[]
            //     {
            //         new BasicAuthProvider(), //Sign-in with HTTP Basic Auth
            //         new CredentialsAuthProvider(), //HTML Form post of UserName/Password credentials
            //     })
            // {
            //     AllowGetAuthenticateRequests = req => true
            // });
            //
            // container.Register<ICacheClient>(new MemoryCacheClient());
            //
            // var userRepo = new InMemoryAuthRepository();
            // var user = new UserAuth
            // {
            //     UserName = "adminuser",
            //     Email = "admin@admin.com",
            //     DisplayName = "Admin User"
            // };
            // userRepo.CreateUserAuth(user, "password");
            // container.Register<IAuthRepository>(userRepo);
            
            SetConfig(new HostConfig
            {
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
            });
        }
    }
}
