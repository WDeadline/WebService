using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class Picture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int DestinationID { get; set; }
        [Required]
        public byte[] Content { get; set; }

        [ForeignKey("DestinationID")]
        public Destination Destination { get; set; }
    }

}
