using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class RoomMatchLogic
    {
        public static Boolean MatchLocation(SeekRoomRequest seekRoomRequest, SeekTenantRequest seekTenantRequest)
        {
            foreach (Location location in seekRoomRequest.RequestLocations)
            {
                if (location.Region.Equals(seekTenantRequest.RoomRegion))
                {
                    return true;
                }
            }
            return false;
        }

        public static Boolean MatchPrice(SeekRoomRequest seekRoomRequest, SeekTenantRequest seekTenantRequest, decimal range)
        {
            range = Math.Abs(range);
            if (seekTenantRequest.MonthlyRentalAmount >= seekRoomRequest.BudgetLowerBound - range &&
                seekTenantRequest.MonthlyRentalAmount <= seekRoomRequest.BudgetUpperBound + range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int GetMatchScoring(SeekRoomRequest seekRoomRequest, SeekTenantRequest seekTenantRequest)
        {
            int scoring = 0;
            if (MatchLocation(seekRoomRequest, seekTenantRequest))
            {
                scoring = scoring + 10;
            }
            for (int i = 0; i <= 10; ++i)
            {
                if (MatchPrice(seekRoomRequest, seekTenantRequest, i * 100))
                {
                    scoring = scoring + (10 - i);
                    break;
                }
            }

            if (seekTenantRequest.Gender.Equals(Gender.NotSpecified) || seekTenantRequest.Gender.Equals(seekRoomRequest.Gender))
            {
                scoring += 10;
            }
            else if (seekRoomRequest.Gender.Equals(Gender.NotSpecified))
            {
                scoring += 5;
            }

            return scoring;
        }
    }
}
