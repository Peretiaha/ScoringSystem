using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.BLL.Services;
using ScoringSystem.DAL.Context;
using ScoringSystem.Infrastructure.ContainerConfiguration;
using ScoringSystem.Web.Authorization;
using ScoringSystem.Web.Mapper;
using System;
using System.Text;

namespace ScoringSystem.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var apiAuthSettings = AddApiAuthSettings(services);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //var connection = @"Server=scoring-system-sql-server.database.windows.net;Database=ScoringSystemDb;User ID=maksym;Password=My2271544;Trusted_Connection=False;Encrypt=True;";
            var connection = @"Server=localhost;Database=ScoringSystemDb;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<ScoringSystemContext>(options => options.UseSqlServer(connection));

            var cookieAuthSettings = AddCookieAuthSettings(services);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.SlidingExpiration = true;
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(cookieAuthSettings.ExpirationTimeInSeconds);
                }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(apiAuthSettings.Secret)),
                        ValidIssuer = apiAuthSettings.Issuer,
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            var builder = new ContainerBuilder();
            builder.RegisterModule(new InfrastructureContainerConfigurator());

            services
             .AddSingleton<ITokenFactory, JwtTokenFactory>()
             .AddTransient<IUserService, UserService>();

            services.AddControllers();
            services.AddCors();

            builder.Populate(services);
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
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
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private ApiAuthSettings AddApiAuthSettings(IServiceCollection services)
        {
            var authSettingsSection = Configuration.GetSection(nameof(ApiAuthSettings));
            services.Configure<ApiAuthSettings>(authSettingsSection);

            return authSettingsSection.Get<ApiAuthSettings>();
        }

        private CookieAuthSettings AddCookieAuthSettings(IServiceCollection services)
        {
            var settingsSection = Configuration.GetSection(nameof(CookieAuthSettings));
            services.Configure<CookieAuthSettings>(settingsSection);

            return settingsSection.Get<CookieAuthSettings>();
        }
    }
}
