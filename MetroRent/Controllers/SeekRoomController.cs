using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using BusinessLogic;
using MetroRent.Models;
using System.Reflection;
using MetroRent.Extensions;
using Microsoft.AspNet.Identity;

namespace MetroRent.Controllers
{
    public class SeekRoomController : Controller
    {
        private MetroRentDBContext db = new MetroRentDBContext();

        // GET: SeekRoom
        public ActionResult Index()
        {

            var query = (from seekRoomRequest in db.SeekRoomRequests
                        select seekRoomRequest)
                        .OrderByDescending(seekRoomRequest => seekRoomRequest.RequestCreateTime);
            
            return View(query.ToList());
        }

        [HttpPost]
        public ActionResult IndexSearchKeyWord(string keyWord)
        {

            if (keyWord.Equals("Description, Name, Phone or Email") || keyWord.Equals(""))
            {
                var query = (from seekRoomRequest in db.SeekRoomRequests
                             select seekRoomRequest)
                        .OrderByDescending(seekRoomRequest => seekRoomRequest.RequestCreateTime);

                return View("Index", query.ToList());
            }
            else
            {
                var query = (from seekRoomRequest in db.SeekRoomRequests
                             where seekRoomRequest.Description.Contains(keyWord) ||
                                    seekRoomRequest.ContactPersonName.Contains(keyWord) ||
                                    seekRoomRequest.ContactPersonPhone.Contains(keyWord) ||
                                    seekRoomRequest.ContactPersonEmail.Contains(keyWord)
                             select seekRoomRequest)
                        .OrderByDescending(seekRoomRequest => seekRoomRequest.RequestCreateTime);

                return View("Index", query.ToList());
            }
        }
        
        [HttpPost]
        public ActionResult Index(string regionFilter)
        {

            if (regionFilter == null || regionFilter.Equals("99"))
            {
                var query = (from seekRoomRequest in db.SeekRoomRequests
                             select seekRoomRequest)
                            .OrderByDescending(seekRoomRequest => seekRoomRequest.RequestCreateTime);

                return View(query.ToList());
            }
            else
            {
                Region region = Region.DowntownManhattan;
                if (regionFilter.Equals("0"))
                {
                    region = Region.DowntownManhattan;
                }
                else if (regionFilter.Equals("1"))
                {
                    region = Region.MidtownManhattan;
                }
                else if (regionFilter.Equals("2"))
                {
                    region = Region.UptownManhattan;
                }
                else if (regionFilter.Equals("3"))
                {
                    region = Region.UpperManhattan;
                }
                else if (regionFilter.Equals("4"))
                {
                    region = Region.RooseveltIsland;
                }
                else if (regionFilter.Equals("5"))
                {
                    region = Region.Brooklyn;
                }
                else if (regionFilter.Equals("6"))
                {
                    region = Region.Queens;
                }
                else if (regionFilter.Equals("7"))
                {
                    region = Region.Bronx;
                }
                else if (regionFilter.Equals("8"))
                {
                    region = Region.StatenIsland;
                }
                else if (regionFilter.Equals("9"))
                {
                    region = Region.NortheastNewJersey;
                }
                else if (regionFilter.Equals("10"))
                {
                    region = Region.WestchesterCounty;
                }
                else if (regionFilter.Equals("11"))
                {
                    region = Region.LongIsland;
                }

                var query = (from seekRoomRequest in db.SeekRoomRequests
                             from location in seekRoomRequest.RequestLocations
                             where location.Region == region
                             select seekRoomRequest)
                            .OrderByDescending(seekRoomRequest => seekRoomRequest.RequestCreateTime);

                return View(query.ToList());
            }
        }

        // GET: SeekRoom/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeekRoomRequest seekRoomRequest = db.SeekRoomRequests.Find(id);

            var p = db.ProfileImages.Select(aa => aa.filePath);
            var profileimg = db.ProfileImages.Where(a => a.Username == seekRoomRequest.Username).Select(b => b.filePath).FirstOrDefault();
            if (profileimg == null)
            {
                profileimg = "~/images/profile/default.png";
            }
            var recpost= db.SeekRoomRequests
                .Where(w=>w.Id!= seekRoomRequest.Id)
                .AsEnumerable()              
                .OrderByDescending(t => t.RequestCreateTime)
                .Select(i => new KeyValuePair<int, string>(i.Id, i.Title));

            ViewBag.List = recpost.ToList();
            ViewBag.ProfileImg = profileimg;
            ViewBag.PostId = id;
            ViewBag.Controller = "SeekRoom";

            if (seekRoomRequest == null)
            {
                return HttpNotFound();
            }
            return View(seekRoomRequest);
        }
        
        //POST: Send Email
        public ActionResult SendEmail(EmailContent content)
        {
            int id = content.PostId;

            using (var db = new MetroRentDBContext())
            {
                var post = db.SeekRoomRequests.Find(id);
                SendEmailLogic.SendEmailtoRoomSeeker(post, content);
            }
            
            return RedirectToAction("Details", "SeekRoom", new { Id = id });
        }

        // GET: SeekRoom/Create
        public ActionResult Create()
        {
            
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register", "Account");
            }
            else 
            {
                SeekRoomRequestViewModel model = new SeekRoomRequestViewModel();
                model.CheckBoxItems = new List<RegionEnumModel>();
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.DowntownManhattan });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.MidtownManhattan });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.UptownManhattan });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.UpperManhattan });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.RooseveltIsland });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.Brooklyn });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.Queens });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.Bronx });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.StatenIsland });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.NortheastNewJersey });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.WestchesterCounty });
                model.CheckBoxItems.Add(new RegionEnumModel() { Region = Region.LongIsland });                
                
                return View(model);
            }
        }

        // POST: SeekRoom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SeekRoomRequestViewModel model)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else if (model.CheckBoxItems == null || !model.CheckBoxItems.Where(p => p.IsSelected).Any())
            {
                ModelState.AddModelError("CheckBoxItems", "Please select atleast one!!!");
                return View("Create", model);
            }
            else if (ModelState.IsValid)
            {

                SeekRoomRequest request = new SeekRoomRequest();

                string currentUserId = User.Identity.GetUserId();
                ApplicationDbContext applicationDb = new ApplicationDbContext();
                ApplicationUser currentUser = applicationDb.Users.FirstOrDefault(x => x.Id == currentUserId);
                if (currentUser.AvatarPath == null)
                {
                    request.ProfileImagePath = "~/Images/Profile/default.png";
                }
                else
                {
                    request.ProfileImagePath = currentUser.AvatarPath;
                }
                applicationDb.Dispose();

                request.Username = User.Identity.Name;
                request.RequestLocations = new List<Location>();
                
                List<string> regionList = new List<string>();
                foreach (RegionEnumModel enumModel in model.CheckBoxItems)
                {
                    if (enumModel.IsSelected)
                    {
                        Location location = new Location();
                        location.Region = enumModel.Region;
                        request.RequestLocations.Add(location);
                        regionList.Add(enumModel.Region.DisplayName());
                    }
                }

                request.Title = "Looking for rooms in ";
                for (int i = 0; i < regionList.Count; ++i)
                {
                    request.Title = request.Title + regionList[i];
                    if ((i != regionList.Count - 1) && regionList.Count != 1)
                    {
                        request.Title = request.Title + ", ";
                    }
                    if (i == regionList.Count - 2)
                    {
                        request.Title = request.Title + "and ";
                    }
                }

                request.BudgetLowerBound = model.BudgetLowerBound;
                request.BudgetUpperBound = model.BudgetUpperBound;
                request.Description = model.Description;
                request.Gender = model.Gender;
                request.RentalStartDate = model.RentalStartDate;
                request.RequestCreateTime = DateTime.Now;
                request.ContactPersonName = model.ContactPersonName;
                request.ContactPersonPhone = model.ContactPersonPhone;
                request.ContactPersonEmail = model.ContactPersonEmail;
                request.IsRequestActive = true;
                
                db.SeekRoomRequests.Add(request);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        
        // GET: SeekRoom/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeekRoomRequest seekRoomRequest = db.SeekRoomRequests.Find(id);
            if (seekRoomRequest == null)
            {
                return HttpNotFound();
            }
            return View(seekRoomRequest);
        }

        // POST: SeekRoom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Title,BudgetLowerBound,BudgetUpperBound,Description,Gender,RentalStartDate,RequestCreateTime,ContactPersonName,ContactPersonPhone,ContactPersonEmail,IsRequestActive")] SeekRoomRequest seekRoomRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seekRoomRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seekRoomRequest);
        }

        // GET: SeekRoom/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeekRoomRequest seekRoomRequest = db.SeekRoomRequests.Find(id);
            if (seekRoomRequest == null)
            {
                return HttpNotFound();
            }
            return View(seekRoomRequest);
        }

        // POST: SeekRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SeekRoomRequest seekRoomRequest = db.SeekRoomRequests.Find(id);
            db.SeekRoomRequests.Remove(seekRoomRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
