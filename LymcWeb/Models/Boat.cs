using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LymcWeb.Models
{
    public class Boat
    {
        public int BoatId { get; set; }
        [MaxLength(64)]
        [Display(Name = "Boat Name")]
        [Required]
        public string BoatName { get; set; }

        [Display(Name = "Image")]
        [MaxLength(256)]
        public string BoatPicUrl { get; set; }

        [MaxLength(1024 * 1024 * 1)]
        public byte[] Picture { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Length (feet)")]
        [Required]
        public float? LengthInFeet { get; set; }

        [MaxLength(32)]
        [Required]
        public string Make { get; set; }

        [Range(1800, 2099)]
        [Display(Name = "Made Year")]
        [Required]
        public int Year { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Added On")]
        public DateTime? RecordCreationDate { get; set; }

       
        [Display(Name = "Added By")]
        public ApplicationUser CreatedBy { get; set; }
    }

}
