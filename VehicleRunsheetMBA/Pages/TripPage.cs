using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using VehicleRunsheetMBA.Models;
using VehicleRunsheetMBAProj.Data.Repositories;
using VehicleRunsheetMBAProj.Mappers;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Pages
{
    public partial class TripPage
    {
        [Inject] 
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUnitOfWork Unit { get; set; }

        [Parameter]
        public string RunsheetId { get; set; }

        private int _id;
        private List<Order> orders;
        private Trip tripFormModel = new Trip();
        private Trip activeTrip = null;
        private Runsheet runsheet;

        protected override async Task OnParametersSetAsync()
        {
            var result = int.TryParse(RunsheetId, out _id);
            runsheet = await Unit.Runsheets.GetByIdAsync(_id);

            var trips = await Unit.Trips.Find(x => x.InProgress && x.RunsheetId == _id);
            activeTrip = trips.FirstOrDefault();

            if (activeTrip != null)
            {
                tripFormModel = TripMapper.MapTrip(activeTrip, tripFormModel);
            }

            if (activeTrip == null)
            {
                activeTrip = new Trip() { InProgress = true };
                tripFormModel.StartTime = DateTime.Now;
                tripFormModel.InProgress = true;
            }
        }

        private async Task HandleSuccess()
        {
            activeTrip = TripMapper.MapTrip(tripFormModel, activeTrip);

            runsheet = await Unit.Runsheets.GetByIdAsync(_id);

            if (activeTrip.Id == 0)
            {
                runsheet.Trips.Add(activeTrip);
                await Unit.Runsheets.UpdateAsync(runsheet);
            }
            else
            {
                await Unit.Trips.UpdateAsync(activeTrip);
            }

            NavigationManager.NavigateTo("/");
        }

        private void HandleStartTime()
        {
            tripFormModel.StartTime = DateTime.Now;
        }

        private void HandleStopTime()
        {
            tripFormModel.EndTime = DateTime.Now;
        }

        private async Task HandleFinalize()
        {
            activeTrip.InProgress = false;
            await Unit.Trips.UpdateAsync(activeTrip);
            NavigationManager.NavigateTo("/runsheet");
        }
    }
}