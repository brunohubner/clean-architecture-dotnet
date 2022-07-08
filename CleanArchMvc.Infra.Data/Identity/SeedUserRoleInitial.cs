using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userMananger;
        private readonly RoleManager<IdentityRole> _roleMananger;

        public SeedUserRoleInitial(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _userMananger = userManager;
            _roleMananger = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleMananger.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";
                IdentityResult roleResult = _roleMananger
                    .CreateAsync(role).Result;
            }

            if (!_roleMananger.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleMananger
                    .CreateAsync(role).Result;
            }
        }

        public async void SeedUsers()
        {
            if (_userMananger.FindByEmailAsync("user@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "user@localhost";
                user.Email = "user@localhost";
                user.NormalizedUserName = "USER@LOCALHOST";
                user.NormalizedEmail = "USER@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userMananger
                    .CreateAsync(user, "@User12345").Result;

                if (result.Succeeded)
                {
                    _userMananger.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (_userMananger.FindByEmailAsync("admin@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userMananger
                    .CreateAsync(user, "@Admin12345").Result;

                if (result.Succeeded)
                {
                    _userMananger.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}