using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    //Being a place that people will make a special trip to visit.
    public class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(250)]
        [Display(Name = "Website Uri")]
        public string WebsiteUri { get; set; }
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [StringLength(250)]
        public string Attributions { get; set; }

        [InverseProperty("Destination")]
        public ICollection<Appreciation> Appreciations { get; set; }
        [InverseProperty("Destination")]
        public ICollection<Picture> Pictures { get; set; }
        [InverseProperty("Destination")]
        public Location Location { get; set; }
    }

}
