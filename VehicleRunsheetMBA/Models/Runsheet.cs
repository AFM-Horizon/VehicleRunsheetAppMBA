using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleRunsheetMBA.Models;

namespace VehicleRunsheetMBAProj.Models
{
    public class Runsheet
    {
        public Runsheet()
        {
            Trips = new List<Trip>();
        }
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool InProgress { get; set; }
        public string Driver { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Choose A Vehicle")]
        public int VehicleDetailsId { get; set; }
        public VehicleDetails VehicleDetails { get; set; }
        public int StartOdometer { get; set; }
        public int EndOdometer { get; set; }
        public DateTime Date { get; set; }
        public List<Trip> Trips { get; set; }
    }
}
