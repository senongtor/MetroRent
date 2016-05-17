using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using MetroRent.Models;
using System.IO;
using MetroRent.Extensions;
using BusinessLogic;

namespace MetroRent.Controllers
{
    public class SeekTenantController : Controller
    {
        private MetroRentDBContext db = new MetroRentDBContext();

        // GET: SeekTenant
        public ActionResult Index()
        {
            var query = (from seekTenantRequest in db.SeekTenantRequests
                         select seekTenantRequest)
                        .OrderByDescending(seekTenantRequest => seekTenantRequest.RequestCreateTime);

            return View(query.ToList());
        }

        [HttpPost]
        public ActionResult IndexSearchKeyWord(string keyWord)
        {

            if (keyWord.Equals("Description, Name, Phone or Email") || keyWord.Equals(""))
            {
                var query = (from seekTenantRequest in db.SeekTenantRequests
                             select seekTenantRequest)
                        .OrderByDescending(seekTenantRequest => seekTenantRequest.RequestCreateTime);

                return View("Index", query.ToList());
            }
            else
            {
                var query = (from seekTenantRequest in db.SeekTenantRequests
                             where seekTenantRequest.Description.Contains(keyWord) ||
                                    seekTenantRequest.ContactPersonName.Contains(keyWord) ||
                                    seekTenantRequest.ContactPersonPhone.Contains(keyWord) ||
                                    seekTenantRequest.ContactPersonEmail.Contains(keyWord)
                             select seekTenantRequest)
                        .OrderByDescending(seekTenantRequest => seekTenantRequest.RequestCreateTime);

                return View("Index", query.ToList());
            }

        }

        [HttpPost]
        public ActionResult Index(string regionFilter)
        {

            if (regionFilter == null || regionFilter.Equals("99"))
            {
                var query = (from seekRoomRequest in db.SeekTenantRequests
                             select seekRoomRequest)
                            .OrderByDescending(seekTenantRequest => seekTenantRequest.RequestCreateTime);

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

                var query = (from seekTenantRequest in db.SeekTenantRequests
                             where seekTenantRequest.RoomRegion == region
                             select seekTenantRequest)
                            .OrderByDescending(seekTenantRequest => seekTenantRequest.RequestCreateTime);

                return View(query.ToList());

            }
        }

        // GET: SeekTenant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeekTenantRequest seekTenantRequest = db.SeekTenantRequests.Find(id);

            var profileimg = db.ProfileImages.Where(s => s.Username == seekTenantRequest.Username).Select(s => s.filePath).FirstOrDefault();
            if (profileimg == null)
            {
                profileimg = "~/images/profile/default.png";
            }
            ViewBag.ProfileImg = profileimg;

            var a=from s in db.RoomImages
                  where s.SeekTenantRequestId ==id            
                  select s.filePath;
            ViewBag.List = a.ToList();

            //Select Similar post, i.e. posts with redard to same region
            var b =db.SeekTenantRequests
                .AsEnumerable()
                .Where(w=>w.RoomType.ToString() == seekTenantRequest.RoomType.ToString() && w.Id!= seekTenantRequest.Id)
                .Select(i => new KeyValuePair<int, string>(i.Id, i.Title));

            ViewBag.Similarlist = b.ToList().Take(5);
            ViewBag.PostId = id;
            ViewBag.Controller = "SeekTenant";

            if (seekTenantRequest == null)
            {
                return HttpNotFound();
            }
            return View(seekTenantRequest);
        }

        //POST: Send EmailContent Model
        public ActionResult SendEmail(EmailContent content)
        {
            int id = content.PostId;

            using (var db = new MetroRentDBContext())
            {
                var post = db.SeekTenantRequests.Find(id);
                SendEmailLogic.SendEmailtoTenantSeeker(post, content);
            }

            return RedirectToAction("Details", "SeekTenant", new { Id = id });
        }

        // GET: SeekTenant/Create
        public ActionResult Create()
        {
            
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register", "Account");
            }
            else
            {
                SeekTenantRequestViewModel request = new SeekTenantRequestViewModel();
                return View(request);
            }
            
        }

        // POST: SeekTenant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SeekTenantRequestViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else if (ModelState.IsValid)
            {
                SeekTenantRequest request = new SeekTenantRequest();
                request.Username = User.Identity.Name;
                request.RoomRegion = model.RoomRegion;
                request.Address = model.Address;
                request.MonthlyRentalAmount = model.MonthlyRentalAmount;
                request.RoomType = model.RoomType;
                request.Gender = model.Gender;
                request.Description = model.Description;
                request.RentalStartDate = model.RentalStartDate;
                request.RequestCreateTime = DateTime.Now;
                request.ContactPersonName = model.ContactPersonName;
                request.ContactPersonPhone = model.ContactPersonPhone;
                request.ContactPersonEmail = model.ContactPersonEmail;
                request.IsRequestActive = true;

                request.Title = "Available room in " + request.RoomRegion.DisplayName() + " for $" + request.MonthlyRentalAmount;

                request.RoomImages = new List<RoomImage>();

                for (int i = 0; i < model.File.Count(); i++)
                {
                    HttpPostedFileBase file = model.File[i];
                    var path = String.Empty;

                    var uploadDir = Server.MapPath("~/Images/RoomImages");
                    //string uploadDir = "~\\Images\\RoomImages";

                    if (file != null && file.ContentLength > 0)
                    {
                        if (!Directory.Exists(uploadDir))
                            Directory.CreateDirectory(uploadDir);
                        string extension = Path.GetExtension(file.FileName);

                        //get a unique guid for file name
                        bool IsUnique = false;
                        string guid = null;

                        while (!IsUnique)
                        {
                            guid = Guid.NewGuid().ToString();
                            var newPath = System.IO.Path.Combine(uploadDir, guid);

                            if (!System.IO.File.Exists(newPath))
                            {
                                IsUnique = true;
                            }
                        }

                        string newName = guid + extension;
                        path = Path.Combine(uploadDir, newName);

                        file.SaveAs(path);

                        RoomImage image = new RoomImage();
                        image.filePath = "~/Images/RoomImages/" + newName;
                        image.SeekTenantRequestId = request.Id;
                        request.RoomImages.Add(image);
                        db.RoomImages.Add(image);
                    }
                }

                if (model.File.Count() == 1 && model.File.First() == null)
                {
                    RoomImage image = new RoomImage();
                    image.filePath = "~/Images/RoomImages/" + "room_default.jpg";
                    image.SeekTenantRequestId = request.Id;
                    request.RoomImages.Add(image);
                    db.RoomImages.Add(image);
                }

                db.SeekTenantRequests.Add(request);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: SeekTenant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeekTenantRequest seekTenantRequest = db.SeekTenantRequests.Find(id);
            if (seekTenantRequest == null)
            {
                return HttpNotFound();
            }
            return View(seekTenantRequest);
        }

        // POST: SeekTenant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Title,RoomRegion,Address,MonthlyRentalAmount,RoomType,Gender,RentalStartDate,RequestCreateTime,ContactPersonName,ContactPersonPhone,ContactPersonEmail,IsRequestActive")] SeekTenantRequest seekTenantRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seekTenantRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seekTenantRequest);
        }

        // GET: SeekTenant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeekTenantRequest seekTenantRequest = db.SeekTenantRequests.Find(id);
            if (seekTenantRequest == null)
            {
                return HttpNotFound();
            }
            return View(seekTenantRequest);
        }

        // POST: SeekTenant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SeekTenantRequest seekTenantRequest = db.SeekTenantRequests.Find(id);
            var images = from c in db.RoomImages
                         where c.SeekTenantRequestId.Equals(id)
                         select c;

            foreach (var img in images)
            {
                string pathToPhoto = img.filePath;
                System.IO.File.Delete(pathToPhoto);
            }

            db.SeekTenantRequests.Remove(seekTenantRequest);
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
