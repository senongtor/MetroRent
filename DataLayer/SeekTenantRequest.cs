using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SeekTenantRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EnumDataType(typeof(Region))]
        [Display(Name = "Room Region")]
        public Region RoomRegion { get; set; }

        public virtual ICollection<RoomImage> RoomImages { get; set; }

        public string Address { get; set; }

        [Required]
        [Display(Name = "Rental")]
        public decimal MonthlyRentalAmount { get; set; }

        public string Title { get; set; }

        [Display(Name = "Description About the Room")]
        public string Description { get; set; }

        [EnumDataType(typeof(RoomType))]
        [Display(Name = "Room Type")]
        public RoomType RoomType { get; set; }

        [EnumDataType(typeof(Gender))]
        [Display(Name = "Anticipated Tenant Gender")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Anticipated Rental Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
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
    }
}
