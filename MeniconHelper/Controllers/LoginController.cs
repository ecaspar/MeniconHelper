using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                if (p.username == loginPerson.username)
                {
                    if(GenerateMD5(loginPerson.password_default) == p.password_default)
                    {
                        return View();
                    }
                    else
                    { 
                        ViewBag.Message = "Incorrect password";

                        return View("Index");
                    }    
                }
            }
            ViewBag.Message = "Incorrect username";

            return View("Index");
        }
        public string GenerateMD5(string rawString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(rawString)).Select(s => s.ToString("x2")));
        }
    }
}