using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRunsheetMBA.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public string OrderNumber { get; set; }
    }
}
