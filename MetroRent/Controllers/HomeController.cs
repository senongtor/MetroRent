using BusinessLogic;
using DataLayer;
using MetroRent.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MetroRent.Controllers
{
    public class HomeController : Controller
    {
        private MetroRentDBContext db = new MetroRentDBContext();
        public ActionResult Index()
        {

            HomeIndexViewModel model = new HomeIndexViewModel();
            model.SeekRoomRequests = new List<SeekRoomRequest>();
            model.SeekTenantRequests = new List<SeekTenantRequest>();

            var query1 = (from seekRoomRequest in db.SeekRoomRequests
                          select seekRoomRequest)
                        .OrderByDescending(seekRoomRequest => seekRoomRequest.RequestCreateTime);

            model.SeekRoomRequests = query1.Take(3).ToList();

            var query2 = (from seekTenantRequest in db.SeekTenantRequests
                          select seekTenantRequest)
                        .OrderByDescending(seekTenantRequest => seekTenantRequest.RequestCreateTime);

            model.SeekTenantRequests = query2.Take(3).ToList();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "MetroRent";

            return View();
        }

        public ActionResult Statistics()
        {

            ViewBag.Message = "Statistics Visualization";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Statistics(DateTime startdate, DateTime enddate, string region, string roomtype)
        {
            var searchre = from s in db.SeekTenantRequests
                           where s.RequestCreateTime >= startdate && s.RequestCreateTime <= enddate && s.RoomRegion.ToString() == region && s.RoomType.ToString() == roomtype
                           select s;

            if (searchre.ToList().Any())
            {
                ViewData["min"] = searchre.ToList().Min(t => t.MonthlyRentalAmount);
                ViewData["max"] = searchre.ToList().Max(t => t.MonthlyRentalAmount);
                ViewData["Avg"] = searchre.ToList().Average(t => t.MonthlyRentalAmount);
                ViewData["Region"] = region;
                ViewData["Type"] = roomtype;
            }
            else
            {
                ViewBag.Found = 1;
            }
            ViewBag.ShowResult = true;
            var l = searchre.OrderByDescending(s => s.RequestCreateTime).Select(s => s.MonthlyRentalAmount).ToList();
            if (l.Any())
            {
                var head = l[0];
                l.Remove(0);
                if (l.Any())
                {
                    var second = l[0];
                    l.Remove(0);
                    if (head > second)
                    {
                        ViewData["trend"] = "Up";
                    }
                    else
                    {
                        ViewData["trend"] = "Down";
                    }
                }
                else
                {
                    ViewData["trend"] = "Remain";
                }            
            }
            else {
                ViewData["trend"] = "Remain";
            }
           
            return View("Statistics", searchre.ToList().FirstOrDefault());
        }

        public ActionResult GenerateChart1(int? id)
        {
            int[] yvalues = new int[12];

            var result =
            from t in db.SeekTenantRequests
            group t by t.RoomRegion into grp
            select grp.Min(ed => ed.MonthlyRentalAmount);

            int j = 0;
            foreach (int i in result)
            {
                yvalues[j++] = i;
            }
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(
                        chartType: "bar",
                        xValue: new[] { "Downtown Manhattan", "MidTown Manhattan", "Uptown Manattan", "Upper Manhattan", "Roosevelt Island", "Brooklyn", "Queens", "Bronx", "Long Island", "Staten Island", "New Jersey", "Westchester County" },
                        yValues: yvalues,
                        axisLabel: "Regions"                  
                        )
                        .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public ActionResult GenerateChart2(int? id)
        {
            int[] yvalues = new int[12];

            var result =
            from t in db.SeekTenantRequests
            group t by t.RoomRegion into grp
            select grp.Average(ed => ed.MonthlyRentalAmount);

            int j = 0;
            foreach (int i in result)
            {
                yvalues[j++] = i;
            }
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(
                        chartType: "bar",
                        xValue: new[] { "Downtown Manhattan", "MidTown Manhattan", "Uptown Manattan", "Upper Manhattan", "Roosevelt Island", "Brooklyn", "Queens", "Bronx", "Long Island", "Staten Island", "New Jersey", "Westchester County" },
                        yValues: yvalues,
                        axisLabel: "Regions"
                        )
                        .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public ActionResult GenerateChart3(int? id)
        {
            int[] yvalues = new int[12];

            var result =
            from t in db.SeekTenantRequests
            group t by t.RoomRegion into grp
            select grp.Max(ed => ed.MonthlyRentalAmount);

            int j = 0;
            foreach (int i in result)
            {
                yvalues[j++] = i;
            }
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(
                        chartType: "bar",
                        xValue: new[] { "Downtown Manhattan", "MidTown Manhattan", "Uptown Manattan", "Upper Manhattan", "Roosevelt Island", "Brooklyn", "Queens", "Bronx", "Long Island", "Staten Island", "New Jersey", "Westchester County" },
                        yValues: yvalues,
                        axisLabel: "Regions"
                        )
                        .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public ActionResult GenerateChart4(int? id)
        {
            int[] yvalues = new int[12];

            var result =
            from t in db.SeekTenantRequests
            group t by t.RoomType into grp
            select grp.Min(ed => ed.MonthlyRentalAmount);

            int j = 0;
            foreach (int i in result)
            {
                yvalues[j++] = i;
            }
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(
                        chartType: "bar",
                        xValue: new[] { "Single Room", "Living Room", "Studio", "1 Bedroom 1 Bathroom", "2 Bedroom 1 Bathroom", "2 Bedroom 2 Bathroom", "3 Bedroom 1 Bathroom", "3 Bedroom 2 Bathroom", "3 Bedroom 3 Bathroom", "4 Bedroom 1 Bathroom", "4 Bedroom 2 Bathroom", "4 Bedroom 3 Bathroom" },
                        yValues: yvalues,
                        axisLabel: "Room Types"
                        )
                        .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public ActionResult GenerateChart5(int? id)
        {
            int[] yvalues = new int[12];

            var result =
            from t in db.SeekTenantRequests
            group t by t.RoomType into grp
            select grp.Average(ed => ed.MonthlyRentalAmount);

            int j = 0;
            foreach (int i in result)
            {
                yvalues[j++] = i;
            }
            var chart = new Chart(width: 300, height: 200)
            .AddSeries( 
                        chartType: "bar",
                        xValue: new[] { "Single Room", "Living Room", "Studio", "1 Bedroom 1 Bathroom", "2 Bedroom 1 Bathroom", "2 Bedroom 2 Bathroom", "3 Bedroom 1 Bathroom", "3 Bedroom 2 Bathroom", "3 Bedroom 3 Bathroom", "4 Bedroom 1 Bathroom", "4 Bedroom 2 Bathroom", "4 Bedroom 3 Bathroom" },
                        yValues: yvalues                 
                        )
                        .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public ActionResult GenerateChart6(int? id)
        {
            int[] yvalues = new int[12];

            var result =
            from t in db.SeekTenantRequests
            group t by t.RoomType into grp
            select grp.Max(ed => ed.MonthlyRentalAmount);

            int j = 0;
            foreach (int i in result)
            {
                yvalues[j++] = i;
            }
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(
                        chartType: "bar",

                        xValue: new[] { "Single Room", "Living Room", "Studio", "1 Bedroom 1 Bathroom", "2 Bedroom 1 Bathroom", "2 Bedroom 2 Bathroom", "3 Bedroom 1 Bathroom", "3 Bedroom 2 Bathroom", "3 Bedroom 3 Bathroom", "4 Bedroom 1 Bathroom", "4 Bedroom 2 Bathroom", "4 Bedroom 3 Bathroom" },
                        yValues: yvalues,
                        axisLabel: "Room Types"

                        )
                        .GetBytes("png");
            return File(chart, "image/bytes");
        }

        //GET: display the account profile page
        public ActionResult AccountProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var currentUserName = User.Identity.GetUserName();
            var currentUserId = User.Identity.GetUserId();
            var avatarPath = string.Empty;
            using (var users = new ApplicationDbContext())
            {
                ApplicationUser user = users.Users.Find(currentUserId);
                avatarPath = user.AvatarPath;
            }

            ProfileViewModel pvm = new ProfileViewModel(currentUserName);
            var db = new MetroRentDBContext();
            pvm.AvatarPath = avatarPath;
            foreach (SeekRoomRequest srr in db.SeekRoomRequests)
            {
                if (srr.Username == currentUserName)
                {
                    pvm.RoomRequests.Add(srr);
                    List<SeekTenantRequest> matches = new List<SeekTenantRequest>();

                    foreach (SeekTenantRequest str in db.SeekTenantRequests)
                    {
                        if (str.IsRequestActive && !str.Username.Equals(currentUserName))
                        {
                            if (RoomMatchLogic.GetMatchScoring(srr, str) >= 23)
                            {
                                matches.Add(str);
                            }
                        }
                    }
                    matches.Take(10);
                    pvm.RoomRequestMatches.Add(matches);
                }
            }

            foreach (SeekTenantRequest str in db.SeekTenantRequests)
            {
                if (str.Username == currentUserName)
                {
                    pvm.TenantRequests.Add(str);
                    List<SeekRoomRequest> matches = new List<SeekRoomRequest>();

                    foreach (SeekRoomRequest srr in db.SeekRoomRequests)
                    {
                        if (str.IsRequestActive && !srr.Username.Equals(currentUserName))
                        {
                            if (RoomMatchLogic.GetMatchScoring(srr, str) >= 23)
                            {
                                matches.Add(srr);
                            }
                        }
                    }
                    matches.Take(10);
                    pvm.TenantRequestMatches.Add(matches);
                }
            }
            return View(pvm);
        }

        //POST: upload profile image
        [HttpPost]
        public ActionResult AccountProfile(HttpPostedFileBase avatar)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");

            }

            var currentUserName = User.Identity.GetUserName();
            var currentUserId = User.Identity.GetUserId();
            var avatarPath = string.Empty;
            using (var db = new ApplicationDbContext())
            {
                ApplicationUser user = db.Users.Find(currentUserId);
                avatarPath = user.AvatarPath;
            }

            var uploadDir = Server.MapPath("~/Images/Profile");
            ProfileImage image = new ProfileImage();

            if (avatar != null && avatar.ContentLength > 0)
            {
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string extension = Path.GetExtension(avatar.FileName);

                bool IsUnique = false;
                string guid = null;
                while (!IsUnique)
                {
                    guid = Guid.NewGuid().ToString();
                    var newPath = Path.Combine(uploadDir, guid);
                    if (!System.IO.File.Exists(newPath))
                    {
                        IsUnique = true;
                    }
                }

                string newName = guid + extension;
                avatarPath = Path.Combine(uploadDir, newName);
                avatar.SaveAs(avatarPath);

                image.filePath = "~/Images/Profile/" + newName;
                image.Username = currentUserName;

                using (var db = new MetroRentDBContext())
                {
                    db.ProfileImages.Add(image);
                    db.SaveChanges();
                }
            }

            using (var users = new ApplicationDbContext())
            {
                ApplicationUser user = users.Users.Find(currentUserId);
                user.AvatarPath = image.filePath;
                users.SaveChanges();
            }

            return RedirectToAction("AccountProfile");
        }
    }
}