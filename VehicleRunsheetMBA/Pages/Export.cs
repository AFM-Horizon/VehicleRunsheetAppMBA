using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRunsheetMBAProj.Data.Repositories;
using VehicleRunsheetMBAProj.Models;
using VehicleRunsheetMBAProj.Utilities;

namespace VehicleRunsheetMBAProj.Pages
{
    public partial class Export
    {
        [Inject] 
        public UserManager<IdentityUser> UserManager { get; set; }

        [Inject] 
        public IUnitOfWork Unit { get; set; }

        [Inject] 
        public IUserInfoProvider UserInfoProvider { get; set; }

        private DateTime _date = DateTime.Now;
        private Runsheet _runsheet;
        private IList<IdentityUser> _users;
        private string _userSelectBoxValue;

        private async Task GetRunsheet()
        {
            var user = await UserInfoProvider.GetUser();
            var userId = await UserInfoProvider.GetId();

            if (user.IsInRole("Manager") || user.IsInRole("Admin"))
            {
                var result = await Unit.Runsheets.GetAllWithChildren();
                _runsheet = result.FirstOrDefault(x => x.Date.Date == _date.Date && x.UserId == _userSelectBoxValue);
            }
            else
            {
                var result = await Unit.Runsheets.GetAllWithChildren();
                _runsheet = result.FirstOrDefault(x => x.UserId == userId && x.Date.Date == _date.Date);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            _users = await UserManager.GetUsersInRoleAsync("User");
        }

        private async Task HandleUserNameChange(ChangeEventArgs e)
        {
            _userSelectBoxValue = e.Value.ToString();
            await GetRunsheet();
        }

        private async Task HandleDateChange(ChangeEventArgs e)
        {
            //TODO Needs To Be Try Parse To Stop Exceptions For Bullshit Dates
            _date = DateTime.Parse(e.Value.ToString());
            await GetRunsheet();
        }
    }
}

