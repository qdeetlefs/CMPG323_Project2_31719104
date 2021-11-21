using CMPG323_Project2.Infrastructures;
using CMPG323_Project2.Models;
using ImageGallery.Data;
using ImageGallery.Data.Models;
using ImageGallery.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG323_Project2
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
            services.AddControllersWithViews();
            services.AddDbContext<ImageGalleryDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICloudinaryImageUpload, CloudinaryImageUpload>();

            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => { options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            })
                .AddOpenIdConnect("Auth0", opt =>
                {
                    // Set the authority to your Auth0 domain
                    opt.Authority = $"https://{Configuration["Auth0:Domain"]}";


                    // Configure the Auth0 Client ID and Client Secret
                    opt.ClientId = Configuration["Auth0:ClientId"];
                    opt.ClientSecret = Configuration["Auth0:ClientSecret"];

                    // Set response type to code
                    opt.ResponseType = OpenIdConnectResponseType.Code;

                    // Configure the scope
                    opt.Scope.Clear();
                    opt.Scope.Add("openid");


                    // Set the callback path, so Auth0 will call back to http://localhost:5000/callback
                    // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard

                    opt.CallbackPath = new PathString("/callback");

                    // Configure the Claims Issuer to be Auth0
                    opt.ClaimsIssuer = "Auth0";


                    opt.Events = new OpenIdConnectEvents
                    {
                        // handle the logout redirection
                        OnRedirectToIdentityProviderForSignOut = (context) =>
                        {
                            var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

                            var postLogoutUri = context.Properties.RedirectUri;
                            if (!string.IsNullOrEmpty(postLogoutUri))
                            {
                                if (postLogoutUri.StartsWith("/"))
                                {
                                    // transform to absolute
                                    var request = context.Request;
                                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                                }
                                logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                            }

                            context.Response.Redirect(logoutUri);
                            context.HandleResponse();

                            return Task.CompletedTask;
                        }
                    };


                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Gallery}/{action=Index}/{id?}");
            });
        }
    }
}
