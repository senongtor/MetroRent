using DataLayer;
using MetroRent.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetroRent.Models
{
    public class SeekTenantRequestViewModel
    {

        [Required]
        [EnumDataType(typeof(Region))]
        [Display(Name = "Room Region")]
        public Region RoomRegion { get; set; }
        
        public string Address { get; set; }

        [EnumDataType(typeof(RoomType))]
        [Display(Name = "Room Type")]
        public RoomType RoomType { get; set; }

        [Required]
        [Display(Name = "Rental Amount")]
        public decimal MonthlyRentalAmount { get; set; }

        [Display(Name = "Description About the Room")]
        public string Description { get; set; }

        [EnumDataType(typeof(Gender))]
        [Display(Name = "Anticipated Tenant Gender")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Anticipated Rental Start Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime RentalStartDate { get; set; }

        [Required]
        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }

        [Phone]
        [Display(Name = "Contact Person Phone")]
        public string ContactPersonPhone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Contact Person Email")]
        public string ContactPersonEmail { get; set; }

        [Display(Name = "Room Pictures")]
        public HttpPostedFileBase[] File { get; set; }

    }
}