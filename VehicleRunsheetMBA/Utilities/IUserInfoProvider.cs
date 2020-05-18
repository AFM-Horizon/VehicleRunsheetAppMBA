using System.Security.Claims;
using System.Threading.Tasks;

namespace VehicleRunsheetMBAProj.Utilities
{
    public interface IUserInfoProvider
    {
        Task<ClaimsPrincipal> GetUser();
        Task<string> GetId();
        Task<string> GetName();
    }
}