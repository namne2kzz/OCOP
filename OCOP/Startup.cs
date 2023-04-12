using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OCOP.Business.Catalog.HoiDongs;
using OCOP.Business.Catalog.HoSos;
using OCOP.Business.Catalog.Huyens;
using OCOP.Business.Catalog.MoHinhSXes;
using OCOP.Business.Catalog.Nganhs;
using OCOP.Business.Catalog.Nhoms;
using OCOP.Business.Catalog.PhanNhoms;
using OCOP.Business.Catalog.TieuChis;
using OCOP.Business.Catalog.Xas;
using OCOP.Business.Common;
using OCOP.Business.System.AppUsers;
using OCOP.Data.Context;
using OCOP.Data.Entities;
using OCOP.ViewModel.Catalog.HoSos;
using OCOP.ViewModel.System.AppUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP
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
            services.AddDbContext<OCOPDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("OCOPSolutionDb")));
            services.AddHttpClient();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Authen/Index/";
                options.AccessDeniedPath = "/Authen/Forbidden/";
            });

            services.AddControllersWithViews()
               .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
            
            services.AddSession(options =>
                       {
                           options.IdleTimeout = TimeSpan.FromMinutes(30);
                       });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddIdentity<AppUser, AppRole>()
               .AddEntityFrameworkStores<OCOPDbContext>()
               .AddDefaultTokenProviders();

            //Declear DI
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IFileStorageService, FileStorageService>();

            services.AddTransient<IHoSoService, HoSoService>();
            services.AddTransient<INganhService, NganhService>();
            services.AddTransient<INhomService, NhomService>();
            services.AddTransient<IPhanNhomService, PhanNhomService>();
            services.AddTransient<IHoiDongService, HoiDongService>();
            services.AddTransient<ITieuChiService, TieuChiService>();
            services.AddTransient<IHuyenService, HuyenService>();
            services.AddTransient<IXaService, XaService>();
            services.AddTransient<IMoHinhSXService, MoHinhSXService>();

            //Fluent validator
            services.AddTransient<IValidator<HoSoCreateRequest>, HoSoCreateRequestValidator>();
            services.AddTransient<IValidator<NguoiDungCreateRequest>, NguoiDungCreateRequestValidator>();

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IMvcBuilder builder = services.AddRazorPages();
#if DEBUG
            if (environment == Environments.Development)
            {
                builder.AddRazorRuntimeCompilation();

            }
#endif
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
                app.UseExceptionHandler("/HomeMember/Error");
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=HomeMember}/{action=Index}/{id?}");
            });
        }
    }
}
