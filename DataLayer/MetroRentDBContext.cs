using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MetroRentDBContext : DbContext
    {

        public MetroRentDBContext() : base("MetroRentDBContext")
        {
        }

        public DbSet<SeekRoomRequest> SeekRoomRequests { get; set; }

        public DbSet<SeekTenantRequest> SeekTenantRequests { get; set; }

        public DbSet<RoomImage> RoomImages { get; set; }

        public DbSet<ProfileImage> ProfileImages { get; set; }

        public DbSet<Location> Locations { get; set; }
        
    }
}
