using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeduCoreApp.Services;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;
using AutoMapper;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.Implementation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using TeduCoreApp.Helpers;
using TeduCoreApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using TeduCoreApp.Authorization;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using TeduCoreApp.Extensions;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Dapper.Interfaces;
using TeduCoreApp.Application.Dapper.Implementation;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using TeduCoreApp.SignalR;
using BotDetect.Web;
using TeduCoreApp.Models;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Data.EF.Repositories;
using TeduCoreApp.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.DependencyModel;
using TeduCoreApp.TokenProviders;

namespace TeduCoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsAssembly("TeduCoreApp.Data.EF")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

            services.AddDefaultIdentity<AppUser>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.Tokens.ProviderMap.Add("CustomEmailConfirmation",
                    new TokenProviderDescriptor(
                        typeof(CustomEmailConfirmationTokenProvider<IdentityUser>)));
                config.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
            })
            .AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<CustomEmailConfirmationTokenProvider<AppUser>>();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;

                //Definde Token
                options.SignIn.RequireConfirmedEmail = true;
            });

            //Services for Files
            services.AddSingleton<IFileProvider>(
                    new PhysicalFileProvider(
                                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddAutoMapper();
            // Add application services.
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddTransient<DbInitializer>();

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                 .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));

            //Repositories
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IProductTagRepository, ProductTagRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IBillRepository, BillRepository>();
            services.AddTransient<IBillDetailRepository, BillDetailRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<ISizeRepository, SizeRepository>();
            services.AddTransient<IProductQuantityRepository, ProductQuantityRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IWholePriceRepository, WholePriceRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IBlogTagRepository, BlogTagRepository>();
            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISystemConfigRepository, SystemConfigRepository>();
            services.AddTransient<IFooterRepository, FooterRepository>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IContactRepository, ContactRepository>();

            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IWardRepository, WardRepository>();
            services.AddTransient<IStreetRepository, StreetRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();

            //Serrvices
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IBlogCategoryService, BlogCategoryService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IDichVuService, DichVuService>();
            services.AddTransient<IDichVuCategoryService, DichVuCategoryService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IProductTrademarkService, ProductTrademarkService>();
            services.AddTransient<IViewRenderService, ViewRenderService>();
            services.AddTransient<IAnnouncementService, AnnouncementService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IFeedbackService, FeedbackService>();

            services.AddTransient<IAuthorizationHandler, BaseResourceAuthorizationHandler>();
            services.AddTransient<IFunctional, Functional>();

            services.AddAuthentication()
                .AddFacebook(facebookOpts =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Facebook");
                    facebookOpts.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOpts.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddGoogle(googleOpts =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");
                    googleOpts.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOpts.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });
            services.Configure<GoogleSettings>(Configuration.GetSection("googlereCaptcha"));
            services.AddRecaptcha(new RecaptchaOptions()
            {
                SiteKey = Configuration["Recaptcha:SiteKey"],
                SecretKey = Configuration["Recaptcha:SecretKey"]
            });
            services.AddSignalR();
            services.AddMinResponse();
            services.AddImageResizer();
            services.AddSession();

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Duration = 120
                    });
                options.CacheProfiles.Add("Never",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            }).AddViewLocalization(
                  LanguageViewLocationExpanderFormat.Suffix,
                  opts => { opts.ResourcesPath = "Resources"; })
              .AddDataAnnotationsLocalization()
              .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddTransient<GooglereCaptchaService>();

            //Serviec for clinent
            services.AddSingleton<IJwtFactory, JwtFactory>();
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;

            //services.AddDirectoryBrowser();
            //services.AddTransient<FilterActionAttribute>();
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();
            services.AddTransient<IAuthorizationHandler, BaseResourceAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/tedu-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseExceptionHandler(
                    builder =>
                    {
                        builder.Run(
                          async context =>
                          {
                              context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                              context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                              var error = context.Features.Get<IExceptionHandlerFeature>();
                              if (error != null)
                              {
                                  context.Response.AddApplicationError(error.Error.Message);
                                  await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                              }
                          });
                    });

            app.UseCaptcha(Configuration);
            app.UseImageResizer();

            app.UseMinResponse();
            app.UseAuthentication();
            app.UseSession();
            app.UseHttpsRedirection();
            //app.UseHttpContextItemsMiddleware();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseFileServer(enableDirectoryBrowsing: true);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
                RequestPath = "/images"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "app", "controllers")),
                RequestPath = "/role"
            });

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseSignalR(routes =>
            {
                routes.MapHub<TeduHub>("/teduHub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}