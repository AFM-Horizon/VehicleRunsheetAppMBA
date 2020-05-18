using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRunsheetMBAProj.Models
{
    public class VehicleDetails
    {
        public VehicleDetails()
        {
            Runsheets = new List<Runsheet>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string VehicleName { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(7)]
        public string Rego { get; set; }

        [Required]
        public VehicleEnums.VehicleType VehicleType { get; set; }

        public ICollection<Runsheet> Runsheets { get; set; }
    }
}