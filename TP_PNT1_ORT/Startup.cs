
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP_PNT1_ORT.Context;

namespace TP_PNT1_ORT
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
            services.AddMvc();
            services.AddControllersWithViews();

            String dbPath= @"filename=./DB/TP1_PNT1_ORT.db";
            services.AddDbContext<UsuariosContext>(options => options.UseSqlite(dbPath));
            services.AddDbContext<GruposContext>(options => options.UseSqlite(dbPath));
            services.AddDbContext<PartidosContext>(options => options.UseSqlite(dbPath));
            services.AddDbContext<UsuariosGruposContext>(options => options.UseSqlite(dbPath));
            services.AddDbContext<ApuestasContext>(options => options.UseSqlite(dbPath));


            services.AddSession();


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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "Login",
                    pattern: "{controller=Login}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "Signup",
                    pattern: "{controller=Singup}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "Grupos",
                    pattern: "{controller=Grupos}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "Partidos",
                    pattern: "{controller=Partidos}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "Usuarios",
                    pattern: "{controller=Usuarios}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "Apuestas",
                    pattern: "{controller=Apuestas}/{action=Index}");
            });
        }
    }
}
