using PersonalWeb.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace PersonalWeb.Models.DataContext
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            User user = new User()
            {
                Name = "Welat",
                Surname = "BARAN",
                Email = "baranvelat021@gmail.com",
                Password = "liceli21",
                ImageURL = "user.png",
                Degree = "Bilgisayar Mühendisi",
                Github = "https://github.com/velatbaran",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5)
            };

            context.User.Add(user);
            context.SaveChanges();
        }
    }
}