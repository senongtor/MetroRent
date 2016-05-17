using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;

namespace MetroRent.Models
{
    public class HomeIndexViewModel
    {
        public List<SeekRoomRequest> SeekRoomRequests { get; set; }

        public List<SeekTenantRequest> SeekTenantRequests { get; set; }
    }
}