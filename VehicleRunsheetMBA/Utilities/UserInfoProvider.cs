using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VehicleRunsheetMBAProj.Utilities
{
    /// <summary>
    /// The <see cref="UserInfoProvider"/> class contains methods to easily obtain information about the currently
    /// logged in application user.  
    /// </summary>
    public class UserInfoProvider : IUserInfoProvider
    {
        private readonly AuthenticationStateProvider _provider;
        private readonly UserManager<IdentityUser> _userManager;

        public UserInfoProvider(AuthenticationStateProvider provider, UserManager<IdentityUser> userManager)
        {
            _provider = provider;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns the the <see cref="Claim"/> with the Id value of the <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetId()
        {
            var authState = await _provider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }

        /// <summary>
        /// Returns a <see cref="Task"/> that, when resolved returns the <see cref="ClaimsPrincipal"/> that describes the current user.
        /// </summary>
        /// <returns></returns>
        public async Task<ClaimsPrincipal> GetUser()
        {
            var authState = await _provider.GetAuthenticationStateAsync();
            return authState.User;
        }

        /// <summary>
        /// Returns a <see cref="Task"/> that, when resolved returns a <see cref="string"/> containing the name of the current user.
        /// </summary>
        /// <returns></returns>
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
