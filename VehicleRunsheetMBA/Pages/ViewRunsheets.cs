using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using VehicleRunsheetMBAProj.Data.Repositories;
using VehicleRunsheetMBAProj.Models;
using VehicleRunsheetMBAProj.Utilities;

namespace VehicleRunsheetMBAProj.Pages
{
    public partial class ViewRunsheets
    {
        [Inject] 
        public IUnitOfWork Unit { get; set; }

        [Inject] 
        public IUserInfoProvider UserInfoProvider { get; set; }

        private List<Runsheet> Runsheets { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var user = await UserInfoProvider.GetUser();
            var userId = await UserInfoProvider.GetId();

            if (user.IsInRole("Manager") || user.IsInRole("Admin"))
            {
                var result = await Unit.Runsheets.GetAllAsync();
                Runsheets = result.ToList();
            }
            else
            {
                var result = await Unit.Runsheets.GetAllAsync();
                Runsheets = result.Where(x => x.UserId == userId).ToList();
            }
        }
    }
}