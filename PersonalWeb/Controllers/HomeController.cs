using PersonalWeb.Helpers;
using PersonalWeb.Models.DataContext;
using PersonalWeb.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PersonalWeb.Controllers
{
    public class HomeController : Controller
    {
        private Repository<User> repoUser = new Repository<User>();
        private Repository<Contact> repoContact = new Repository<Contact>();
        private Repository<ProjectsDone> repoProjectsDone = new Repository<ProjectsDone>();
        private Repository<Services> repoServices = new Repository<Services>();

        [Route("Anasayfa")]
        public ActionResult Index()
        {
            ViewBag.UserData = repoUser.FirstData();
            ViewBag.Contact = repoContact.FirstData();
            return View();
        }

        public ActionResult _PartialProjectsDone()
        {
            return PartialView(repoProjectsDone.ListIQeryable().Include("Services").OrderByDescending(x => x.CreatedOn).ToList());
        }

        public ActionResult GetProjectsDoneDetail(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectsDone projetsDone = repoProjectsDone.Find(x => x.Id == id.Value);
            if(projetsDone == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialPopupProjectsDoneDetail", projetsDone);
        }

        [Route("ProjeDetay/{projectname}/{id:int}")]
        public ActionResult ProjectsDoneDetail(int? id)
        {
            ViewBag.UserData = repoUser.FirstData();
            ViewBag.Contact = repoContact.FirstData();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectsDone projetsDone = repoProjectsDone.Find(x => x.Id == id.Value);
            if(projetsDone == null)
            {
                return HttpNotFound();
            }

            return View(projetsDone);
        }

        [Route("HizmetDetay/{baslik}/{id:int}")]
        public ActionResult ServicesDetail(int? id)
        {
            ViewBag.UserData = repoUser.FirstData();
            ViewBag.Contact = repoContact.FirstData();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Services services = repoServices.Find(x => x.Id == id.Value);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        [HttpPost]
        public ActionResult ContactingClients(string name , string email , string phone, string message)
        {
            ViewBag.UserData = repoUser.FirstData();
            ViewBag.Contact = repoContact.FirstData();

            if (message == null || name == null || email == null)
            {
                return Json(new { hasError = true, Message = "Lütfen zorunlu alanları doldurunuz." });
            }

            string body = $"Merhaba <br><br> Ben : {name}; <br><br> Telefonum : {phone}; <br><br> Email Adres: {email};<br><br> Mesajım : {message}";

            MailHelper.SendMail(body, repoUser.FirstData().Email, "Mesajınız ", true);

            return Json(new { hasError = false , Message = "Mesajınız başarılı bir şekilde gönderilmiştir."});
        }
    }
}