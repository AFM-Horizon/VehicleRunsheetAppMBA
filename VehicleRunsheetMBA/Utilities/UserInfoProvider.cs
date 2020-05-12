using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VehicleRunsheetMBAProj.Utilities
{
    public class UserInfoProvider : IUserInfoProvider
    {
        private readonly AuthenticationStateProvider _provider;

        public UserInfoProvider(AuthenticationStateProvider provider)
        {
            _provider = provider;
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
    }
}
