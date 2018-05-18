using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    //the recognition and enjoyment of the good qualities of someone or something.
    public class Appreciation
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        [Display(Name = "Application User")]
        public int ApplicationUserID { get; set; }
        [Key]
        [Required]
        [Column(Order = 1)]
        [Display(Name = "Destination")]
        public int DestinationID { get; set; }
        [Required]
        [StringLength(250)]
        public string Content { get; set; }
        [Required]
        [Range(0, 5)]
        public double Rating { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("DestinationID")]
        public Destination Destination { get; set; }
    }

}
