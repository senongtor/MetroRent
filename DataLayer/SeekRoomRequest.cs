using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{

    public class SeekRoomRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
    
        [Required]
        public virtual ICollection<Location> RequestLocations { get; set; }

        [Display(Name = "Rental Budget Lower Bound")]
        public decimal BudgetLowerBound { get; set; }

        [Display(Name = "Rental Budget Upper Bound")]
        public decimal BudgetUpperBound { get; set; }

        public string Title { get; set; }

        [Display(Name = "Description about the Tenant")]
        public string Description { get; set; }

        [EnumDataType(typeof(Gender))]
        [Display(Name = "Gender of the Tenant")]
        [Required]
        public Gender Gender { get; set; }

        [Display(Name = "Anticipated Rental Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RentalStartDate { get; set; }

        [Display(Name = "Request Create Time")]
        public DateTime RequestCreateTime { get; set; }

        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }

        [Display(Name = "Contact Person Phone")]
        public string ContactPersonPhone { get; set; }

        [Display(Name = "Contact Person Email")]
        public string ContactPersonEmail { get; set; }

        [Display(Name = "Is Request Active")]
        public Boolean IsRequestActive { get; set; }

        [Display(Name = "Profile Image Path")]
        public string ProfileImagePath { get; set; }

    }
}
