using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class AuthenticateService : IAutheticate
    {
        private readonly UserManager<ApplicationUser> _userMananger;
        private readonly SignInManager<ApplicationUser> _signInMananger;
        public AuthenticateService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
        )
        {
            _userMananger = userManager;
            _signInMananger = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInMananger.PasswordSignInAsync(
                email,
                password,
                false,
                lockoutOnFailure: false
            );

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInMananger.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userMananger.CreateAsync(applicationUser, password);

            if (result.Succeeded)
            {
                await _signInMananger.SignInAsync(
                    applicationUser,
                    isPersistent: false
                );
            }

            return result.Succeeded;
        }
    }
}