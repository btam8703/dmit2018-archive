using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Additional Namespaces
using eRaceProject.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Configuration;
#endregion

namespace eRaceProject.Security
{
    public class SecurityDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {
            #region Seed the roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            foreach (var role in startupRoles)
                roleManager.Create(new IdentityRole { Name = role });
            #endregion

            #region Seed the users
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminRole = ConfigurationManager.AppSettings["adminRole"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail,
                OfficeManagerID = null,
                ClerkID = null,
                FoodServiceID = null,
                DirectorID = null

            }, adminPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, adminRole);
           
            //Director Alex Newland, Employee Number 24
            string directorUser = "NewlandA";
            string directorRole = "Director";
            string directorPassword = ConfigurationManager.AppSettings["newUserPassword"];
            int directorID = 24;
            result = userManager.Create(new ApplicationUser
            {
                UserName = directorUser,
                Email = "Newlanda@eRace.ca",
                DirectorID = directorID,
                OfficeManagerID = null,
                ClerkID = null,
                FoodServiceID = null

            }, directorPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(directorUser).Id, directorRole);

            //Office Manager Karen Yates, Employee Number 12
            string officemanagerUser = "YatesK";
            string officemanagerRole = "OfficeManager";
            string officemanagerPassword = ConfigurationManager.AppSettings["newUserPassword"];
            int officemanagerID = 12;
            result = userManager.Create(new ApplicationUser
            {
                UserName = officemanagerUser,
                OfficeManagerID = officemanagerID,
                Email = "YatesK@eRace.ca",
                DirectorID = null,
                ClerkID = null,
                FoodServiceID = null

            }, officemanagerPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(officemanagerUser).Id, officemanagerRole);

            //Clerk James Calder, Employee Number 20
            string clerkUser = "CalderJ";
            string clerkRole = "Clerk";
            string clerkPassword = ConfigurationManager.AppSettings["newUserPassword"];
            int clerkID = 20;
            result = userManager.Create(new ApplicationUser
            {
                UserName = clerkUser,
                ClerkID = clerkID,
                Email = "CalderJ@eRace.ca",
                DirectorID = null,
                OfficeManagerID = null,
                FoodServiceID = null
            }, clerkPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(clerkUser).Id, clerkRole);

            //Food Service Employee Marla Kreeg, Employee Number 1
            string foodserviceUser = "KreegM";
            string foodserviceRole = "FoodService";
            string foodservicePassword = ConfigurationManager.AppSettings["newUserPassword"];
            int foodserviceID = 1;
            result = userManager.Create(new ApplicationUser
            {
                UserName = foodserviceUser,
                FoodServiceID = foodserviceID,
                Email = "KreegM@eRace.ca",
                DirectorID = null,
                OfficeManagerID = null,
                ClerkID = null,
                
            }, foodservicePassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(foodserviceUser).Id, foodserviceRole);
            #endregion
            // ... etc. ...





            base.Seed(context);
        }
    }

}