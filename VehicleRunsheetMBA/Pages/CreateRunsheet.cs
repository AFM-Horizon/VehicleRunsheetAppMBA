using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.CodeAnalysis.CSharp;
using VehicleRunsheetMBAProj.Data.Repositories;
using VehicleRunsheetMBAProj.Models;
using VehicleRunsheetMBAProj.Utilities;

namespace VehicleRunsheetMBAProj.Pages
{
    public partial class CreateRunsheet
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUnitOfWork Unit { get; set; }

        [Inject]
        public IUserInfoProvider UserInfoProvider { get; set; }

        private Runsheet runsheetViewModel = new Runsheet() { InProgress = true };
        private string vehicleId = "";
        private Runsheet active = null;
        private IEnumerable<VehicleDetails> vehicles = new List<VehicleDetails>();
        private bool isActiveTrip;

        protected override async Task OnInitializedAsync()
        {
            var user = await UserInfoProvider.GetUser();
            var userId = await UserInfoProvider.GetId();
            vehicles = await Unit.Vehicles.GetAllAsync();

            var allRunsheets = await Unit.Runsheets.GetAllWithChildren();
            active = allRunsheets.FirstOrDefault(x => x.UserId == userId && x.InProgress);
            runsheetViewModel.Driver = await UserInfoProvider.GetName();

            if (active != null)
            {
                runsheetViewModel.Trips = active.Trips;
                runsheetViewModel.UserId = active.UserId;
                runsheetViewModel.InProgress = active.InProgress;
                runsheetViewModel.Date = active.Date;
                runsheetViewModel.Id = active.Id;
                runsheetViewModel.Driver = active.Driver;
                runsheetViewModel.StartOdometer = active.StartOdometer;
                runsheetViewModel.EndOdometer = active.EndOdometer;
                runsheetViewModel.VehicleDetails = active.VehicleDetails;
                vehicleId = active.VehicleDetails.Id.ToString();
            }

            var result = active?.Trips.Find(x => x.InProgress);
            if (result != null)
            {
                isActiveTrip = true;
            }

            if (active == null)
            {
                active = new Runsheet()
                {
                    InProgress = true,
                    UserId = userId,
                    Driver = await UserInfoProvider.GetName()
                };
            }
        }

        private async Task HandleSuccess()
        {
            active.Trips = runsheetViewModel.Trips;
            active.InProgress = runsheetViewModel.InProgress;
            active.Date = runsheetViewModel.Date;
            active.Driver = runsheetViewModel.Driver;
            active.StartOdometer = runsheetViewModel.StartOdometer;
            active.EndOdometer = runsheetViewModel.EndOdometer;
            active.VehicleDetails = runsheetViewModel.VehicleDetails;

            if (int.TryParse(vehicleId, out var id))
            {
                var vehicle = await Unit.Vehicles.GetByIdAsync(id);
                vehicle.Runsheets.Add(active);
                await Unit.Vehicles.UpdateAsync(vehicle);
            }

            if (active.Id == 0)
            {
                await Unit.Runsheets.AddAsync(active);
            }
            //else
            //{
            //    await Unit.Runsheets.UpdateAsync(active);
            //}

            NavigationManager.NavigateTo($"Trip/{active.Id}");
        }

        private async Task HandleFinalize()
        {
            active.Date = DateTime.Today;
            active.InProgress = false;
            await Unit.Runsheets.UpdateAsync(active);
            runsheetViewModel = new Runsheet();
        }
    }
}