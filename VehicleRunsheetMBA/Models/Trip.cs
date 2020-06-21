using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
using VehicleRunsheetMBAProj.Models;
using VehicleRunsheetMBAProj.Utilities;

namespace VehicleRunsheetMBA.Models
{
    public class Trip
    {
        public Trip()
        {
            Orders = new List<Order>();
        }

        [Key]
        public int Id { get; set; }
        public int RunsheetId { get; set; }
        public Runsheet Runsheet { get; set; }
        public bool InProgress { get; set; }
        public string Customer { get; set; }
        [MaxLength(5)]
        public List<Order> Orders { get; set; }
        [MaxLength(20)]
        public string ReceivedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
