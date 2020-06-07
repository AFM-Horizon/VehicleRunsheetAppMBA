using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        public string CurrentOrder { get; set; }

        private int _id;
        private List<Order> orders;
        private Trip tripFormModel = new Trip();
        private Trip activeTrip = null;
        private Runsheet runsheet;
        private EditContext editContext;
        private bool canShowValidationSummary = false;

        protected override async Task OnParametersSetAsync()
        {
            editContext = new EditContext(tripFormModel);

            var result = int.TryParse(RunsheetId, out _id);
            runsheet = await Unit.Runsheets.GetByIdAsync(_id);

            activeTrip = await Unit.Trips.GetTripWithChildrenByRunsheetId(_id);

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

        private async Task HandleReturn()
        {
            tripFormModel.Orders.Add(new Order() { OrderNumber = "RETURN" });
            tripFormModel.ReceivedBy = "N/A";
            tripFormModel.Customer = "M.B.A";
            tripFormModel.EndTime = DateTime.Now;
        }

        private async Task UpdateModel()
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
        }

        private async Task HandleUpdate()
        {
            editContext.MarkAsUnmodified();
            await UpdateModel();
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

        private void AddOrder()
        {

            tripFormModel.Orders.Add(new Order() { OrderNumber = CurrentOrder });
            CurrentOrder = "";

        }

        private void HandleDeleteOrder(string orderNumber)
        {
            var order = tripFormModel.Orders.Find(x => x.OrderNumber == orderNumber);
            tripFormModel.Orders.Remove(order);
        }

        private async Task HandleFinalize()
        {
            if (!editContext.Validate())
            {
                canShowValidationSummary = true;
            }
            if (editContext.Validate())
            {
                canShowValidationSummary = false;
                await UpdateModel();
                activeTrip.InProgress = false;
                await Unit.Trips.UpdateAsync(activeTrip);
                NavigationManager.NavigateTo("/");
            }
        }
    }
}