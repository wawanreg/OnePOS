﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using OnePOS.Models;
using OnePOS.Models.Dashboard.Brand;
using OnePOS.Models.Invoice;

namespace OnePOS
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(
        IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }
        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(
                new RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>()));
        }
    }

    //// This is useful if you do not want to tear down the database each time you run the application.
    //// public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    //// This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {//Create User=Admin@Admin.com with password=Admin@123456 in the Admin role     
        protected override void Seed(ApplicationDbContext context)
        {
            var brandCategory = new List<BrandCategoryModels>
            {
               new BrandCategoryModels{
                   BrandCategoryId = 1,
                   BrandCategoryName  = "Body Parts"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 2,
                   BrandCategoryName  = "Brakes"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 3,
                   BrandCategoryName  = "Controls"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 4,
                   BrandCategoryName  = "Covers"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 5,
                   BrandCategoryName  = "Dash and Gauges"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 6,
                   BrandCategoryName  = "Drive"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 7,
                   BrandCategoryName  = "Engine Parts and Accessories"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 8,
                   BrandCategoryName  = "Exhaust"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 9,
                   BrandCategoryName  = "Fuel and Air"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 10,
                   BrandCategoryName  = "Light and Electrial"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 11,
                   BrandCategoryName  = "Suspension"
               },
               new BrandCategoryModels{
                   BrandCategoryId = 12,
                   BrandCategoryName  = "Tire and Wheels"
               }
            };
            brandCategory.ForEach(x => context.BrandCategory.Add(x));
            context.SaveChanges();

            var billingStatusCat = new List<BillingStatusModel>
            {
               new BillingStatusModel{
                   BillingStatusId = 1,
                   BillingName  = "Send"
               },
               new BillingStatusModel{
                   BillingStatusId = 2,
                   BillingName  = "Paid"
               },
               new BillingStatusModel{
                   BillingStatusId = 3,
                   BillingName  = "Pending"
               },
               new BillingStatusModel{
                   BillingStatusId = 4,
                   BillingName  = "Late"
               }
            };
            billingStatusCat.ForEach(x => context.BillingStatus.Add(x));
            context.SaveChanges();

            ///////////////////////////////////////////
            InitializeIdentityForEF(context);
            base.Seed(context);
        }
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string fullname = "EdyGunawan";
            const string phonenumber = "081977937885";

            string[] roleName = new string[] { "Admin", "Store Manager", "Cashier", "Super Admin" };
            //Create Role Admin if it does not exist

            for (var i = 0; i < roleName.Length; i++)
            {
                var role = roleManager.FindByName(roleName[i]);
                if (role == null)
                {
                    role = new ApplicationRole(roleName[i]);
                    var roleresult = roleManager.Create(role);
                }
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, FullName = fullname, PhoneNumber = phonenumber };
                var result = userManager.Create(user, password);
                user.CreatedBy = name;
                user.CreatedDate = DateTime.UtcNow;
                user.UpdatedBy = name;
                user.UpdatedDate = user.CreatedDate;

                user.EmailConfirmed = true;
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains("Super Admin"))
            {
                var result = userManager.AddToRole(user.Id, "Super Admin");
            }

        }
    }


    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
