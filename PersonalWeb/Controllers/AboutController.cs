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
    public class AboutController : Controller
    {
        Repository<About> repo = new Repository<About>();

        [Route("yonetimpaneli/hakkimizda")]
        public ActionResult Index()
        {
            return View(repo.FirstData());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            About about = repo.Find(x => x.Id == id.Value);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(About about, HttpPostedFileBase ImageURL)
        {
            ModelState.Remove("ModifiedOn");
            if (ModelState.IsValid)
            {
                About a = repo.Find(x => x.Id == about.Id);
                if (a == null)
                {
                    return HttpNotFound();
                }

                if (ImageURL != null && (ImageURL.ContentType == "image/png" ||
                                         ImageURL.ContentType == "image/jpg" ||
                                         ImageURL.ContentType == "image/jpeg"))
                {
                    string filename = $"about_{about.Id}.{ImageURL.ContentType.Split('/')[1]}";
                    ImageURL.SaveAs(Server.MapPath($"~/images/about/{filename}"));
                    a.ImageURL = filename;
                }
                a.Text = about.Text;
                repo.Update(a);

                return RedirectToAction("Index");
            }
            return View(about);
        }
    }
}