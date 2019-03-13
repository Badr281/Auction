using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using vente.Models;

[assembly: OwinStartupAttribute(typeof(vente.Startup))]
namespace vente
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRools();
            CreateUsers();
        }
        public void CreateUsers()
        {


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.Email = "Badr@gmail.com";
            user.UserName = "badr";
            var check = userManager.Create(user, "12345A&a");
            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admins");

            }
          
        }
        public void CreateRools()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;


            if (!roleManager.RoleExists("Admins"))
            {
                role = new IdentityRole();
                role.Name = "Admins";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Vendeur"))
            {
                role = new IdentityRole();
                role.Name = "Vendeur";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Acheteur"))
            {
                role = new IdentityRole();
                role.Name = "Acheteur";
                roleManager.Create(role);
            }
        }
    }
}
