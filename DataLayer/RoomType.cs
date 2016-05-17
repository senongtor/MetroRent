using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public enum RoomType
    {
        [Display(Name = "Single Room")]
        SingleRoom = 0,

        [Display(Name = "Living Room")]
        LivingRoom = 1,

        [Display(Name = "Studio")]
        Studio = 2,

        [Display(Name = "1 Bedroom 1 Bathroom")]
        OneBedroomOneBathroom = 3,

        [Display(Name = "2 Bedroom 1 Bathroom")]
        TwoBedroomOneBathroom = 4,

        [Display(Name = "2 Bedroom 2 Bathroom")]
        TwoBedroomTwoBathroom = 5,

        [Display(Name = "3 Bedroom 1 Bathroom")]
        ThreeBedroomOneBathroom = 6,

        [Display(Name = "3 Bedroom 2 Bathroom")]
        ThreeBedroomTwoBathroom = 7,

        [Display(Name = "3 Bedroom 3 Bathroom")]
        ThreeBedroomThreeBathroom = 8,

        [Display(Name = "4 Bedroom 1 Bathroom")]
        FourBedroomOneBathroom = 9,

        [Display(Name = "4 Bedroom 2 Bathroom")]
        FourBedroomTwoBathroom = 10,

        [Display(Name = "4 Bedroom 3 Bathroom")]
        FourBedroomThreeBathRoom = 11,

        [Display(Name = "Others")]
        Others = 12

    }
}
