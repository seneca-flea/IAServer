using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using IA.Controllers;

namespace IA.Models
{
    public static class IdentityInitialize
    {
        // Load user accounts
        public static async void LoadUserAccounts()
        {
            //// Remove the following statement when you are ready to 
            //// actually ready to use this method
            //return;

            // Get a reference to the objects we need
            var ds = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ds));

            // Add the user(s) that the app needs when loaded for the first time
            // Change any of the data below to better match your app's needs
            if (userManager.Users.Count() == 0)
            {
                // Account Administrator
                var user = new ApplicationUser { UserName = "admin@myseneca.ca", Email = "admin@myseneca.ca" };
                var result = await userManager.CreateAsync(user, "AccountAdmin123$");
                if (result.Succeeded)
                {
                    // Add claims
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Email, "admin@myseneca.ca"));
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "AccountAdministrator"));
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, "Account"));
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, "Administrator"));
                }

                // SenecaFlea Administrator
                var secondUser = new ApplicationUser { UserName = "senecaflea@myseneca.ca", Email = "senecaflea@myseneca.ca" };
                var secondResult = await userManager.CreateAsync(secondUser, "SenecaFleaAdmin123$");
                if (secondResult.Succeeded)
                {
                    // Add claims
                    await userManager.AddClaimAsync(secondUser.Id, new Claim(ClaimTypes.Email, "senecaflea@myseneca.ca"));
                    await userManager.AddClaimAsync(secondUser.Id, new Claim(ClaimTypes.Role, "SenecaFleaAdministrator"));
                    await userManager.AddClaimAsync(secondUser.Id, new Claim(ClaimTypes.GivenName, "SenecaFlea"));
                    await userManager.AddClaimAsync(secondUser.Id, new Claim(ClaimTypes.Surname, "Administrator"));
                }
            }
        }

        // Load app claims

        public static void LoadAppClaims()
        {
            //// Remove the following statement when you are ready to 
            //// actually ready to use this method
            //return;

            // Get a reference to the manager
            Manager m = new Manager();

            // If there are no claims, add them
            if (m.AppClaimGetAll().Count() == 0)
            {
                // Add claims here
                AppClaimAdd r1 = new AppClaimAdd();
                r1.ClaimType = "role";
                r1.ClaimValue = "AccountAdministrator";
                r1.ClaimTypeUri = "http://senecaflea.azurewebsites.net/role";
                r1.Description = "Account Administrator";
                m.AppClaimAdd(r1);

                AppClaimAdd r2 = new AppClaimAdd();
                r2.ClaimType = "role";
                r2.ClaimTypeUri = "http://senecaflea.azurewebsites.net/role";
                r2.ClaimValue = "SenecaFleaAdministrator";
                r2.Description = "Seneca Flea Administrator";
                m.AppClaimAdd(r2);

                AppClaimAdd r3 = new AppClaimAdd();
                r3.ClaimType = "role";
                r3.ClaimTypeUri = "http://senecaflea.azurewebsites.net/role";
                r3.ClaimValue = "User";
                r3.Description = "A normal user who would be a seller or a buyer";
                m.AppClaimAdd(r3);
            }
        }

    }
}
