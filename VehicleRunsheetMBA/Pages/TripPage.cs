using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using VehicleRunsheetMBA.Models;
using VehicleRunsheetMBAProj.Components.ComponentServices;
using VehicleRunsheetMBAProj.Data.Repositories;
using VehicleRunsheetMBAProj.Mappers;
using VehicleRunsheetMBAProj.Models;
using VehicleRunsheetMBAProj.Utilities;

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
        private Trip _tripFormModel = new Trip();
        private Trip _activeTrip = null;
        private Runsheet _runsheet;
        private EditContext _editContext;
        private bool _canShowValidationSummary = false;

        protected override async Task OnParametersSetAsync()
        {
            _editContext = new EditContext(_tripFormModel);

            var result = int.TryParse(RunsheetId, out _id);
            _runsheet = await Unit.Runsheets.GetByIdAsync(_id);

            _activeTrip = await Unit.Trips.GetTripWithChildrenByRunsheetId(_id);

            if (_activeTrip != null)
            {
                _tripFormModel = TripMapper.MapTrip(_activeTrip, _tripFormModel);
            }

            if (_activeTrip == null)
            {
                _activeTrip = new Trip() { InProgress = true };
                _tripFormModel.StartTime = LocalTimeUtility.GetLocalTime();
                _tripFormModel.InProgress = true;
            }
        }

        private async Task HandleReturn()
        {
            _tripFormModel.Orders.Add(new Order() { OrderNumber = "RETURN" });
            _tripFormModel.ReceivedBy = "N/A";
            _tripFormModel.Customer = "M.B.A";
            _tripFormModel.EndTime = LocalTimeUtility.GetLocalTime();
        }

        private async Task UpdateModel()
        {
            _activeTrip = TripMapper.MapTrip(_tripFormModel, _activeTrip);
            _runsheet = await Unit.Runsheets.GetByIdAsync(_id);

            if (_activeTrip.Id == 0)
            {
                _runsheet.Trips.Add(_activeTrip);
                await Unit.Runsheets.UpdateAsync(_runsheet);
            }
            else
            {
                await Unit.Trips.UpdateAsync(_activeTrip);
            }
        }

        private async Task HandleUpdate()
        {
            _editContext.MarkAsUnmodified();
            await UpdateModel();
            NavigationManager.NavigateTo("/");
        }

        private void HandleStartTime()
        {
            _tripFormModel.StartTime = LocalTimeUtility.GetLocalTime();
        }

        private void HandleStopTime()
        {
            _tripFormModel.EndTime = LocalTimeUtility.GetLocalTime();
        }

        private void AddOrder()
        {

            _tripFormModel.Orders.Add(new Order() { OrderNumber = CurrentOrder });
            CurrentOrder = "";

        }

        private void HandleDeleteOrder(string orderNumber)
        {
            var order = _tripFormModel.Orders.Find(x => x.OrderNumber == orderNumber);
            _tripFormModel.Orders.Remove(order);
        }

        private async Task HandleFinalize()
        {
            if (!_editContext.Validate())
            {
                _canShowValidationSummary = true;
            }
            if (_editContext.Validate())
            {
                _canShowValidationSummary = false;
                await UpdateModel();
                _activeTrip.InProgress = false;
                await Unit.Trips.UpdateAsync(_activeTrip);
                NavigationManager.NavigateTo("/");
            }
        }
    }
}