using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    //a particular place or position.
    public class Location
    {
        [Key]
        [Required]
        [Display(Name = "Destination")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DestinationID { get; set; }
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [ForeignKey("DestinationID")]
        public Destination Destination { get; set; }
    }

}
