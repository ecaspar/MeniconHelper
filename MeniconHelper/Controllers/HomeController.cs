using System.Web.Mvc;
using MeniconHelper.Models;

namespace MeniconHelper.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(Session["User"]!=null)
            {
                person p = (person)(Session["User"]);
                ViewBag.name = p.first_name + " " + p.last_name;

                ViewBag.Incident = loadIncident();

                return View();
            }
            else
                return View("../Login/Index");
        }


        public ListIncident loadIncident()
        {
            MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities();
            ListIncident listIncident = new ListIncident();
            foreach(incident i in meniconHelperEntities.incident)
            {
                listIncident.Reference = i.incident_code;
                listIncident.Image = "Une image";
                listIncident.Description = i.description;
                listIncident.Supervisor = "?";
                listIncident.Date = i.date_create;
                listIncident.Declarant = i.person.first_name + " " + i.person.last_name;
                listIncident.Type = i.statut.label;
                listIncident.Area = i.area.name;
                listIncident.Engine = i.engine.name;

            }
            return listIncident;
        }
    }
}