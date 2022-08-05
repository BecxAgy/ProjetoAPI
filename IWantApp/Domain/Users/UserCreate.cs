using System.Security.Claims;

namespace IWantApp.Domain.Users
{
    public class UserCreate
    {
        private UserManager<IdentityUser> userManager;

        public UserCreate(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<(IdentityResult, string)> Create(string Email, string password, List<Claim> claims)
        {

            var newUser = new IdentityUser { UserName = Email, Email = Email };

            var result = await userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
                return (result, String.Empty);


            return (await userManager.AddClaimsAsync(newUser, claims), newUser.Id);
        }
    }
}
