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
using Microsoft.Extensions.Options;
using TeduCoreApp.SignalR;
using BotDetect.Web;
using TeduCoreApp.Models;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Data.EF.Repositories;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using TeduCoreApp.Admin.Filter;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TeduCoreApp
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
            #region Identity

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsAssembly("TeduCoreApp.Data.EF")));

            services.AddIdentity<AppUser, AppRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/notify-denied.html");
            });

            //Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings

                //Definde Token
                options.SignIn.RequireConfirmedEmail = true;
                //email duy nhất
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });

            #endregion Identity

            #region EFAndMap

            services.AddAutoMapper();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));
            services.AddTransient<DbInitializer>();

            #endregion EFAndMap

            #region Repositories

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
            services.AddTransient<IShipCodeRepository, ShipCodeRepository>();
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IWardRepository, WardRepository>();
            services.AddTransient<IStreetRepository, StreetRepository>();
            services.AddTransient<IFeedbacksRepository, FeedbacksRepository>();

            #endregion Repositories

            #region Serrvices

            //Services for Files
            services.AddSingleton<IFileProvider>(
                    new PhysicalFileProvider(
                                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            //Serrvices
            services.AddTransient<IAddressService, AddressService>();
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
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<IShipCodeService, ShipCodeService>();
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<ISizeService, SizeService>();

            #endregion Serrvices

            #region Fuctions

            services.AddTransient<IFunctional, Functional>();
            services.AddSingleton<IEmailSender, EmailSender>();

            #endregion Fuctions

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
            })
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               .AddViewLocalization(
                  LanguageViewLocationExpanderFormat.Suffix,
                  opts => { opts.ResourcesPath = "Resources"; })
              .AddDataAnnotationsLocalization()
              .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
              .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
              .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddTransient<GooglereCaptchaService>();
            services.AddMvcCore();
            services.AddCors();

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

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
            app.UseExceptionHandler
            (
              builder =>
                    {
                        builder.Run(
                          async context =>
                          {
                              context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                              context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                              var error = context.Features.Get<IExceptionHandlerFeature>(); if (error != null)
                              {
                                  context.Response.AddApplicationError(error.Error.Message); await
                                  context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                              }
                          });
                    }
            );

            app.UseCaptcha(Configuration);
            app.UseImageResizer();
            app.UseStaticFiles();
            app.UseMinResponse();
            app.UseAuthentication();
            app.UseSession();
            app.UseHttpsRedirection();

            //app.UseHttpContextItemsMiddleware();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
            //    RequestPath = "/MyImages"
            //});

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
            //    RequestPath = "/images"
            //});

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //      Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "app", "controllers")),
            //    RequestPath = "/role"
            //});

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCookiePolicy();

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