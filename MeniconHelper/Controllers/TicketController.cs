using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeniconHelper.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                person p = (person)(Session["User"]);
                ViewBag.name = p.first_name + " " + p.last_name;

                return View();
            }
            else
                return View("../Login/Index");
        }
    }
}