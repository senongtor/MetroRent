using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetroRent.Models
{
    public class ProfileViewModel
    {
        [Key]
        [Display(Name = "Registered Email")]
        public string UserName { get; set; }

        public HttpPostedFileBase Avatar { get; set; }

        public string AvatarPath { get; set; }

        public IList<SeekRoomRequest> RoomRequests { get; set; }
        public IList<List<SeekTenantRequest>> RoomRequestMatches { get; set; }
        public IList<SeekTenantRequest> TenantRequests { get; set; }
        public IList<List<SeekRoomRequest>> TenantRequestMatches { get; set; }

        public ProfileViewModel(string name)
        {
            UserName = name;
            RoomRequests = new List<SeekRoomRequest>();
            RoomRequestMatches = new List<List<SeekTenantRequest>>();
            TenantRequests = new List<SeekTenantRequest>();
            TenantRequestMatches = new List<List<SeekRoomRequest>>();
        }
    }
}