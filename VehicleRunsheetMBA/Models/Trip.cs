using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBA.Models
{
    public class Trip
    {
        private DateTime startTime;
        private DateTime endTime;

        public Trip()
        {
            Orders = new List<Order>();
        }

        [Key]
        public int Id { get; set; }
        public int RunsheetId { get; set; }
        public Runsheet Runsheet { get; set; }
        public bool InProgress { get; set; }

        public DateTime StartTime
        {
            get => startTime.AddHours(10);
            set => startTime = value.AddHours(-10);
        }

        public DateTime EndTime
        {
            get => endTime.AddHours(10); 
            set => endTime = value.AddHours(-10);
        }

        public string Customer { get; set; }
        [MaxLength(5)]
        public List<Order> Orders { get; set; }
        [MaxLength(20)]
        public string ReceivedBy { get; set; }
    }
}
