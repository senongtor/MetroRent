using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{


    public enum Region
    {
        [Display(Name = "Downtown Manhattan")]
        DowntownManhattan = 0,

        [Display(Name = "Midtown Manhattan")]
        MidtownManhattan = 1,

        [Display(Name = "Uptown Manhattan")]
        UptownManhattan = 2,

        [Display(Name = "Upper Manhattan")]
        UpperManhattan = 3,

        [Display(Name = "Roosevelt Island")]
        RooseveltIsland = 4,

        [Display(Name = "Brooklyn")]
        Brooklyn = 5,

        [Display(Name = "Queens")]
        Queens = 6,

        [Display(Name = "Bronx")]
        Bronx = 7,

        [Display(Name = "Staten Island")]
        StatenIsland = 8,

        [Display(Name = "Northeast New Jersey")]
        NortheastNewJersey = 9,

        [Display(Name = "Westchester County")]
        WestchesterCounty = 10,

        [Display(Name = "Long Island")]
        LongIsland = 11

    }

}
