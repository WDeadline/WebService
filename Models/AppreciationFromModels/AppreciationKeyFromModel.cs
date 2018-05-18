using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.AppreciationFromModels
{
    public class AppreciationKeyFromModel
    {
        [Required]
        [Display(Name = "Application User")]
        public int ApplicationUserID { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public int DestinationID { get; set; }
    }
}
