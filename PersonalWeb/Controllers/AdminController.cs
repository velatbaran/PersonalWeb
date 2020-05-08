using PersonalWeb.Filters;
using PersonalWeb.Helpers;
using PersonalWeb.Models.DataContext;
using PersonalWeb.Models.Entity;
using PersonalWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PersonalWeb.Controllers
{
    public class AdminController : Controller
    {
        Repository<User> repo = new Repository<User>();
        Repository<Services> repoServices = new Repository<Services>();
        Repository<ProjectsDone> repoProjectsDone = new Repository<ProjectsDone>();

        [Route("yonetimpaneli")]
        [Auth]
        public ActionResult Index()
        {
            ViewBag.ServicesCount = repoServices.List().Count();
            ViewBag.ProjectsDoneCount = repoProjectsDone.List().Count();
            return View();
        }

        [Route("yonetimpaneli/profil")]
        [Auth]
        public ActionResult ShowProfile()
        {
            return View(repo.FirstData());
        }

        [Auth]
        [HttpGet]
        public ActionResult EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = repo.Find(x => x.Id == id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditProfile(User user, HttpPostedFileBase ImageURL)
        {
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                User u = repo.Find(x => x.Id == user.Id);
                if (u == null)
                {
                    return HttpNotFound();
                }

                if (ImageURL != null && (ImageURL.ContentType == "image/png" ||
                                         ImageURL.ContentType == "image/jpg" ||
                                         ImageURL.ContentType == "image/jpeg"))
                {
                    string filename = $"user_{user.Id}.{ImageURL.ContentType.Split('/')[1]}";
                    ImageURL.SaveAs(Server.MapPath($"~/images/user/{filename}"));
                    u.ImageURL = filename;
                }
                u.Name = user.Name;
                u.Surname = user.Surname;
                u.Degree = user.Degree;
                u.Email = user.Email;
                u.Github = user.Github;
                if(user.Password != null) 
                    u.Password = Crypto.Hash(user.Password.ToString(), "MD5");
                repo.Update(u);

                Session["login"] = u;
                return RedirectToAction("/yonetimpaneli/profil");
            }

            return View(user);
        }

        [Route("yonetimpaneli/giris")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string pass = Crypto.Hash(model.Password.ToString(), "MD5");
                User user = repo.Find(x => x.Email == model.Email && x.Password == pass);

                if(user == null)
                {
                    ViewBag.Info = "Email adresi yada Şifre bilgileri hatalı";
                    return View(model);
                }

                Session["login"] = user.Email;
                return RedirectToAction("yonetimpaneli");
            }

            return View(model);
        }

        [Route("yonetimpaneli/sifremiunuttum")]
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ForgetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                User user = repo.Find(x => x.Email == model.Email);
                if(user == null)
                {
                    ViewBag.Info = "Sistemde böyle bir kullanıcı kayıtlı değil.";
                    return View(model);
                }

                int newNumber = 0;
                Random rnd = new Random();
                newNumber = rnd.Next();
                user.Password = Crypto.Hash(Convert.ToString(newNumber), "MD5");
                repo.Update(user);

                string body = $"Merhaba : {user.Name} {user.Surname}; <br><br> Yeni Şifreniz : {newNumber}";
                MailHelper.SendMail(body, user.Email, "Kişisel Web Sitem - Yeni Şifre Talebi", true);
                ViewBag.Info = "Yeni şifreniz başarılı bir şekilde gönderilmiştir.";

                return View("ForgetPassword");
            }
            return View(model);
        }

        [Auth]
        public ActionResult LogOut()
        {
            Session.Clear();
            return View("/yonetimpaneli/giris");
        }

    }
}