using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OCOP.Business.Catalog.HoiDongs;
using OCOP.Business.Catalog.HoSos;
using OCOP.Business.Catalog.Huyens;
using OCOP.Business.Catalog.MoHinhSXes;
using OCOP.Business.Catalog.Xas;
using OCOP.Business.Common;
using OCOP.Business.System.AppRoles;
using OCOP.Business.System.AppUsers;
using OCOP.Data.Context;
using OCOP.Data.Entities;
using OCOP.ViewModel.Catalog.HoiDongs;
using OCOP.ViewModel.System.AppUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP.Admin
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
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IFileStorageService, FileStorageService>();

            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IAppRoleService, AppRoleService>();

            services.AddTransient<IHuyenService, HuyenService>();
            services.AddTransient<IXaService, XaService>();
            services.AddTransient<IMoHinhSXService, MoHinhSXService>();
            services.AddTransient<IHoiDongService, HoiDongService>();
            services.AddTransient<IHoSoService, HoSoService>();

            //Fluent Validator
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<IValidator<ThanhVienCreateRequest>, ThanhVienCreateRequestValidator>();
            services.AddTransient<IValidator<HoiDongCreateRequest>, HoiDongCreateRequestValidator>();




            //string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            //string signingKey = Configuration.GetValue<string>("Tokens:Key");
            //byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(option =>
            //    {
            //        option.RequireHttpsMetadata = false;
            //        option.SaveToken = true;
            //        option.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateIssuer = true,
            //            ValidIssuer = issuer,
            //            ValidateAudience = true,
            //            ValidAudience = issuer,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ClockSkew = System.TimeSpan.Zero,
            //            IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
            //        };
            //    });

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
                app.UseExceptionHandler("/Home/Error");
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
