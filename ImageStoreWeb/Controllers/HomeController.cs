using ImageStoreWeb.Infrastructure;
using ImageStoreWeb.Models;
using ImageStoreWeb.Repositories;
using ImageStoreWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImageStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repos = new ClientRepository();
                var user = await repos.FindAsync(model.UserName);
                if (user == null)
                {

                    var newUser = new Client()
                    {
                        UserName = model.UserName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Phone = model.Phone,
                        Password = AppHelper.GetHash(model.Password),
                        Country = model.Country,
                        State = model.State,
                        City = model.City,
                        Street = model.Street,
                        Zip = model.Zip,
                        Active = false,
                        DateRegistered = DateTime.UtcNow
                    };
                    await repos.AddAsync(newUser);

                    return View("RegisterSuccess");
                }
                ModelState.AddModelError("UserName", "The user exist, please try another user name");
            }


            return View(model);
        }

    }
}