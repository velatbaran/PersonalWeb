using PersonalWeb.Filters;
using PersonalWeb.Models.DataContext;
using PersonalWeb.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PersonalWeb.Controllers
{
    [Auth]
    public class ServicesController : Controller
    {
       private Repository<Services> repo = new Repository<Services>();

        [Route("yonetimpaneli/hizmetler")]
        public ActionResult Index()
        {
            return View(repo.ListIQeryable().OrderByDescending(x=>x.CreatedOn));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Services services, HttpPostedFileBase ImageURL)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");

            if (ModelState.IsValid)
            {
                if (ImageURL != null && (ImageURL.ContentType == "image/png" ||
                                          ImageURL.ContentType == "image/jpg" ||
                                          ImageURL.ContentType == "image/jpeg"))
                {
                    string filename = $"services_{services.Title}.{ImageURL.ContentType.Split('/')[1]}";
                    ImageURL.SaveAs(Server.MapPath($"~/images/services/{filename}"));
                    services.ImageURL = filename;
                }
                repo.Insert(services);
                return RedirectToAction("Index");
            }

            return View(services);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Services services = repo.Find(x => x.Id == id.Value);
            if (services == null)
            {
                return HttpNotFound();
            }

            return View(services);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Services services, HttpPostedFileBase ImageURL)
        {
            ModelState.Remove("ModifiedOn");

            if (ModelState.IsValid)
            {
                Services s = repo.Find(x => x.Id == services.Id);

                if (ImageURL != null && (ImageURL.ContentType == "image/png" ||
                                          ImageURL.ContentType == "image/jpg" ||
                                          ImageURL.ContentType == "image/jpeg"))
                {
                    string filename = $"services_{services.Id}.{ImageURL.ContentType.Split('/')[1]}";
                    ImageURL.SaveAs(Server.MapPath($"~/images/services/{filename}"));
                    s.ImageURL = filename;
                }
                s.Title = services.Title;
                s.Text = services.Text;

                repo.Update(s);
                return RedirectToAction("Index");
            }

            return View(services);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Services services = repo.Find(x => x.Id == id.Value);
            if (services == null)
            {
                return HttpNotFound();
            }

            repo.Delete(services);
            System.IO.File.Delete(Server.MapPath($"~/images/services/{services.ImageURL}"));
            return RedirectToAction("Index");
        }
    }
}