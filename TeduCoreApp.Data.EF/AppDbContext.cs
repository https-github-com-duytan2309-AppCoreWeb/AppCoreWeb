using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF.Extensions;
using TeduCoreApp.Data.EF.Configurations;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TeduCoreApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TeduCoreApp.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Address> Address { set; get; }
        public DbSet<ShipCode> ShipCodes { set; get; }
        public DbSet<Language> Languages { set; get; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<AppUserActions> AppUserActions { set; get; }
        public DbSet<AppUserClaim> AppUserClaims { get; set; }
        public DbSet<AppUserLogin> AppUserLogins { get; set; }
        public DbSet<AppRoleClaim> AppRoleClaims { get; set; }
        public DbSet<AppUserToken> AppUserTokens { get; set; }

        public DbSet<ListAction> ListActions { set; get; }
        public DbSet<ListController> ListControllers { set; get; }
        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }
        public DbSet<Bill> Bills { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }
        public DbSet<Blog> Blogs { set; get; }
        public DbSet<BlogTag> BlogTags { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTrademark> ProductTrademarks { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductQuantity> ProductQuantities { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Size> Sizes { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<WholePrice> WholePrices { get; set; }
        public DbSet<AdvertistmentPage> AdvertistmentPages { get; set; }
        public DbSet<Advertistment> Advertistments { get; set; }
        public DbSet<AdvertistmentPosition> AdvertistmentPositions { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<DichVuCategory> DichVuCategories { get; set; }
        public DbSet<DichVuTag> DichVuTags { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<Province> Provinces { set; get; }
        public DbSet<District> Districts { set; get; }
        public DbSet<Ward> Wards { set; get; }
        public DbSet<Street> Streets { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=TAN-IT\\SqlExpress;Database=TeduCore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Identity Config

            builder.Entity<AppUserClaim>().ToTable("AppUserClaims").HasKey(x => x.Id);

            builder.Entity<AppUserClaim>().ToTable("AppUserClaims")
               .HasOne<AppUser>(sc => sc.AppUser)
               .WithMany(s => s.AppUserClaims)
               .HasForeignKey(sc => sc.UserId);

            builder.Entity<AppRoleClaim>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);

            builder.Entity<AppUserLogin>().ToTable("AppUserLogins").HasKey(l => new { l.UserId, l.LoginProvider, l.ProviderKey });

            builder.Entity<AppUserLogin>().ToTable("AppUserLogins")
               .HasOne<AppUser>(sc => sc.AppUser)
               .WithMany(s => s.AppUserLogins)
               .HasForeignKey(sc => sc.UserId);

            builder.Entity<AppUserToken>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });

            //AspNetUserRole

            builder.Entity<AppUser>().ToTable("AppUsers").HasKey(x => x.Id);
            builder.Entity<AppRole>().ToTable("AppRoles").HasKey(x => x.Id);

            builder.Entity<AppUserRole>().ToTable("AppUserRoles");

            builder.Entity<AppUserRole>().ToTable("AppUserRoles")
               .HasOne<AppUser>(sc => sc.AppUsers)
               .WithMany(s => s.AppUserRoles)
               .HasForeignKey(sc => sc.UserId);

            builder.Entity<AppUserRole>().ToTable("AppUserRoles")
              .HasOne<AppRole>(sc => sc.AppRoles)
              .WithMany(s => s.AppUserRoles)
              .HasForeignKey(sc => sc.RoleId);

            #endregion Identity Config

            builder.Entity<AppUserActions>().ToTable("AppUserActions").HasKey(sc => new { sc.IdAction, sc.IdUser });

            builder.Entity<AppUserActions>().ToTable("AppUserActions")
                .HasOne<ListAction>(sc => sc.ListAction)
                .WithMany(s => s.AspNetUserActions)
                .HasForeignKey(sc => sc.IdAction);

            builder.Entity<AppUserActions>().ToTable("AppUserActions")
                .HasOne<AppUser>(sc => sc.AppUser)
                .WithMany(s => s.AppUserActions)
                .HasForeignKey(sc => sc.IdUser);

            builder.AddConfiguration(new TagConfiguration());
            builder.AddConfiguration(new BlogTagConfiguration());
            builder.AddConfiguration(new ContactDetailConfiguration());
            builder.AddConfiguration(new FooterConfiguration());
            builder.AddConfiguration(new PageConfiguration());
            builder.AddConfiguration(new FooterConfiguration());
            builder.AddConfiguration(new ProductTagConfiguration());
            builder.AddConfiguration(new SystemConfigConfiguration());
            builder.AddConfiguration(new AdvertistmentPositionConfiguration());
            builder.AddConfiguration(new DichVuTagConfiguration());
            //base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.DateCreated = DateTime.Now;
                    }
                    changedOrAddedItem.DateModified = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}