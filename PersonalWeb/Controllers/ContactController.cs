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
    public class ContactController : Controller
    {
        Repository<Contact> repo = new Repository<Contact>();

        [Route("yonetimpaneli/iletisim")]
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

            Contact contact = repo.Find(x => x.Id == id.Value);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Contact contact)
        {
            ModelState.Remove("ModifiedOn");
            if (ModelState.IsValid)
            {
                Contact c = repo.Find(x => x.Id == contact.Id);
                if (c == null)
                {
                    return HttpNotFound();
                }

                c.Adressess = contact.Adressess;
                c.Email = contact.Email;
                c.Instagram = contact.Instagram;
                c.Phone = contact.Phone;
                c.Twitter = contact.Phone;
                c.Telegram = contact.Telegram;
                c.Youtube = contact.Youtube;
                c.Facebook = contact.Facebook;
                repo.Update(c);

                return RedirectToAction("Index");
            }
            return View(contact);
        }
    }
}