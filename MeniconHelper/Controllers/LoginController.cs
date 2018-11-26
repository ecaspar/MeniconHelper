using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeniconHelper.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(person loginPerson)
        {
            MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities();
            
            foreach(person p in meniconHelperEntities.person)
            {
                if (p.username == loginPerson.username && p.password_default == loginPerson.password_default)
                {
                    return View();
                }
            }

            return RedirectToAction("Index");

        }
    }
}