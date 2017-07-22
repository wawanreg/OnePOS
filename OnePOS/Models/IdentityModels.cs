using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnePOS.Models.Dashboard;
using OnePOS.Models.Dashboard.Items;
using OnePOS.Models.Dashboard.Vendors;

namespace OnePOS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Active = true;
            Deleted = false;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public ClaimsIdentity GenerateUserIdentity(UserManager<ApplicationUser> manager)
        {
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }
        public string CreatedAgent { get; set; }
        public string UpdatedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedDate { get; set; }
        public string UpadtedAgent { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("DefaultConnection"));
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("DbControl"));
            //Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Create The Table
            modelBuilder.Entity<VendorViewModels>().ToTable(("Vendor"));
            modelBuilder.Entity<ItemViewModels>().ToTable(("Item"));
            modelBuilder.Entity<BrandViewModels>().ToTable(("Brand"));
            modelBuilder.Entity<ManufacturerViewModels>().ToTable(("Manufacturer"));

        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<VendorViewModels> Vendor { get; set; }
        public DbSet<ItemViewModels> Item { get; set; }
        public DbSet<BrandViewModels> Brand { get; set; }
        public DbSet<ManufacturerViewModels> Manufacturer { get; set; }

    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
        public string Description { get; set; }

    }
}