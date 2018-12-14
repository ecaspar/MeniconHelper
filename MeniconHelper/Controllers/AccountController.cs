using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MeniconHelper.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            //Someone who isn't logged can't access to this view.
            if (Session["User"] != null)
            {
                //Get which user is logged.
                person p = (person)(Session["User"]);
                ViewBag.name = p.first_name + " " + p.last_name;
                ViewBag.person = p;

                if (p.id_role == 0)
                    ViewBag.Admin = true;
                else
                    ViewBag.Admin = false;

                return View();
            }
            else
                return View("../Login/Index");
        }

        [HttpPost]
        public ActionResult ChangePassword(person person, string newPassword, string confirmPassword)
        {
            MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities();
            person p = (person)(Session["User"]);
            if (p.password_default == GenerateMD5(person.password_default))
            {
                if(newPassword == confirmPassword)
                {
                    //Update password de p
                    person pswPerson = meniconHelperEntities.person.Single(x => x.id_person == p.id_person);
                    pswPerson.password_default = GenerateMD5(confirmPassword);
                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Account/Index");
        }

        public string GenerateMD5(string rawString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(rawString)).Select(s => s.ToString("x2")));
        }
    }
}