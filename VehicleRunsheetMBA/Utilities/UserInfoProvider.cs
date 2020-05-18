using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VehicleRunsheetMBAProj.Utilities
{
    public class UserInfoProvider : IUserInfoProvider
    {
        private readonly AuthenticationStateProvider _provider;
        private readonly UserManager<IdentityUser> _userManager;

        public UserInfoProvider(AuthenticationStateProvider provider, UserManager<IdentityUser> userManager)
        {
            _provider = provider;
            _userManager = userManager;
        }

        public async Task<string> GetId()
        {
            var authState = await _provider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }

        public async Task<ClaimsPrincipal> GetUser()
        {
            var authState = await _provider.GetAuthenticationStateAsync();
            return authState.User;
        }

        public async Task<string> GetName()
        {
            var authState = await GetClaimsPrincipal();
            return authState.FindFirst("UserName").Value;
        }

        private async Task<ClaimsPrincipal> GetClaimsPrincipal()
        {
            var authState = await _provider.GetAuthenticationStateAsync();
            return authState.User;
        }
    }
}
