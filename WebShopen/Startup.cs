using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using WebShopen.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebShopen
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            // .AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<JwtBearerOptions>(
             AzureADB2CDefaults.JwtBearerAuthenticationScheme, options =>
             {
                 options.TokenValidationParameters.NameClaimType = "name";
             });
            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                .AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));
            services.AddControllersWithViews();
            services.AddRazorPages();
            ///////////////////////////////////KOPIERAT////////////////////////////////////////////////////
            // Configuration to sign-in users with Azure AD B2C

            //services.AddMicrosoftIdentityWebAppAuthentication(Configuration, "AzureAdB2C");
            //services.AddControllersWithViews()
            //    .AddMicrosoftIdentityUI();
            //services.AddRazorPages();
            ////Configuring appsettings section AzureAdB2C, into IOptions
            //services.AddOptions();
            //services.Configure<OpenIdConnectOptions>(Configuration.GetSection("AzureAdB2C"));
            /////////////////////////////////////////////////////////////////////////////////////////////
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                 .AddCookie(options =>
                 {
                     options.LoginPath = "/account/google-login";
                 })
                .AddGoogle(options =>
                {
                    options.ClientId = "479215895037-p0ujudllhjlfsc9evm6i3nogbhmpitvc.apps.googleusercontent.com";
                    options.ClientSecret = "KuKsHYQ0B36QqQLXsC9L3FC_";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
