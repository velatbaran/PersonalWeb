using PersonalWeb.Filters;
using PersonalWeb.Models.DataContext;
using PersonalWeb.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PersonalWeb.Controllers
{
    [Auth]
    public class ProjectsDoneController : Controller
    {
        private Repository<ProjectsDone> repoProjectsDone = new Repository<ProjectsDone>();
        private Repository<Services> repoServices = new Repository<Services>();

        [Route("yonetimpaneli/yapilanprojeler")]
        public ActionResult Index()
        {
            return View(repoProjectsDone.ListIQeryable().Include("Services").OrderByDescending(x=>x.CreatedOn).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ServicesId = new SelectList(repoServices.List(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ProjectsDone projectsDone, HttpPostedFileBase ImageURL)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");

            if (ModelState.IsValid)
            {
                if (ImageURL != null && (ImageURL.ContentType == "image/jpg" ||
                                            ImageURL.ContentType == "image/png" ||
                                            ImageURL.ContentType == "image/jpeg"))
                {
                    string filename1 = $"projectsdone_{projectsDone.Customer}.{ImageURL.ContentType.Split('/')[1]}";
                    ImageURL.SaveAs(Server.MapPath($"~/images/projectsdone/{filename1}"));
                    projectsDone.ImageURL = filename1;
                }

                repoProjectsDone.Insert(projectsDone);
                return RedirectToAction("Index");
            }

            ViewBag.ServicesId = new SelectList(repoServices.List(), "Id", "Title", projectsDone.ServicesId);
            return View(projectsDone);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectsDone projectsdone = repoProjectsDone.Find(x => x.Id == id.Value);
            if(projectsdone == null)
            {
                return HttpNotFound();
            }

            ViewBag.ServicesId = new SelectList(repoServices.List(), "Id", "Title", projectsdone.ServicesId);
            return View(projectsdone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ProjectsDone projectsDone, HttpPostedFileBase ImageURL)
        {
            ModelState.Remove("ModifiedOn");

            if (ModelState.IsValid)
            {
                ProjectsDone pd = repoProjectsDone.Find(x => x.Id == projectsDone.Id);

                if (ImageURL != null && (ImageURL.ContentType == "image/jpg" ||
                                            ImageURL.ContentType == "image/png" ||
                                            ImageURL.ContentType == "image/jpeg"))
                {
                    string filename1 = $"projectsdone_{projectsDone.Customer}.{ImageURL.ContentType.Split('/')[1]}";
                    ImageURL.SaveAs(Server.MapPath($"~/images/projectsdone/{filename1}"));
                    pd.ImageURL = filename1;
                }

                pd.ProjectName = projectsDone.ProjectName;
                pd.Customer = projectsDone.Customer;
                pd.Teknologies = projectsDone.Teknologies;
                pd.ProjectDoneDate = projectsDone.ProjectDoneDate;
                pd.Description = projectsDone.Description;
                pd.ServicesId = projectsDone.ServicesId;

                repoProjectsDone.Update(pd);
                return RedirectToAction("Index");
            }

            ViewBag.ServicesId = new SelectList(repoServices.List(), "Id", "Title", projectsDone.ServicesId);
            return View(projectsDone);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectsDone projectsDone = repoProjectsDone.Find(x => x.Id == id.Value);
            if (projectsDone == null)
            {
                return HttpNotFound();
            }
            repoProjectsDone.Delete(projectsDone);
            System.IO.File.Delete(Server.MapPath($"~/images/projectsdone/{projectsDone.ImageURL}"));
            return RedirectToAction("Index");
        }
    }
}