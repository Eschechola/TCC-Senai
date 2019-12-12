using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyfr.DAL.Interfaces;
using Lyfr.DAL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lyfr
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opcoes =>
                {
                    opcoes.LoginPath = "/Home/Login";
                    opcoes.LogoutPath = "/Home/Logout";
                });
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IRepositoryCliente, RepositoryCliente>();
            services.AddTransient<IRepositorySugestao, RepositorySugestao>();
            services.AddTransient<IRepositoryGenero, RepositoryGenero>();
            services.AddTransient<IRepositoryLivro, RepositoryLivro>();
            services.AddTransient<IRepositoryGeral, RepositoryGeral>();
            services.AddTransient<IRepositoryHistorico, RepositoryHistorico>();
            services.AddTransient<IRepositoryFavoritos, RepositoryFavoritos>();
            services.AddDirectoryBrowser();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://lyfrapi.com.br",
                                    "http://lyfradmin.vbweb.com.br",
                                    "http://lyfr.com.br", 
                                    "http://www.lyfrapi.com.br",
                                    "http://www.lyfradmin.vbweb.com.br",
                                    "http://www.lyfr.com.br", 
                                    "http://localhost:49642")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
            });
            app.UseStatusCodePagesWithReExecute("/Errors/Index", "?statusCode={0}");
            app.UseSession();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            var provider = new FileExtensionContentTypeProvider();
            // Add new mappings
            provider.Mappings[".epub"] = "application/epub+zip";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider

            });
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
