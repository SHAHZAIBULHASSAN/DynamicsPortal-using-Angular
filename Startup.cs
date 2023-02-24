namespace DynamicsPortal
{
    using DynamicsPortal.Business;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.PowerPlatform.Dataverse.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class Startup
    {
        IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        void AddBusinessManagers(IServiceCollection services)
        {
            services.AddTransient<IContactManager, ContactManager>();
            services.AddTransient<ILeadManager, LeadManager>();
        }

        #region "Infrastructure"
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.Secure = CookieSecurePolicy.Always;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/connect/login";
                    options.LogoutPath = "/connect/logout";
                    options.AccessDeniedPath = string.Empty;
                });

            services.AddAuthorization(options => options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
            services.AddControllersWithViews();

           // services.AddScoped(sp => new ServiceClient(Configuration.GetConnectionString("DefaultConnection")));
            AddBusinessManagers(services);
        }        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());
            app.UseSpa(spa =>
            {
                spa.ApplicationBuilder.MapWhen(context => !context.User.Identity.IsAuthenticated, builder => builder.Run(async context => await context.ChallengeAsync(CookieAuthenticationDefaults.AuthenticationScheme)));

                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "ClientApp";
                    spa.UseAngularCliServer("start");
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
        #endregion
    }
}
