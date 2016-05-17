using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetroRent.Models
{

    public class RegionEnumModel
    {
        public RegionEnumModel(DataLayer.Region Region, Boolean IsSelected)
        {
            this.Region = Region;
            this.RegionName = Region.ToString();
            this.IsSelected = IsSelected;
        }
        
        public RegionEnumModel()
        {
        }

        public DataLayer.Region Region { get; set; }

        public string RegionName { get; set; }

        public Boolean IsSelected { get; set; }
    }

    public class SeekRoomRequestViewModel
    {
        [Display(Name = "The regions of the rooms you are looking for:")]
        public List<RegionEnumModel> CheckBoxItems { get; set; }

        [Display(Name = "Rental Budget Lower Bound")]
        public decimal BudgetLowerBound { get; set; }

        [Display(Name = "Rental Budget Upper Bound")]
        public decimal BudgetUpperBound { get; set; }

        [Display(Name = "Description about the Tenant")]
        public string Description { get; set; }

        [EnumDataType(typeof(Gender))]
        [Display(Name = "Gender of the Tenant")]
        [Required]
        public Gender Gender { get; set; }

        [Display(Name = "Anticipated Rental Start Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
        
    }
}