using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeniconHelper.Models;

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

                ViewBag.Ticket = LoadTicket();

                return View();
            }
            else
                return View("../Login/Index");
        }

        public ListIncident LoadTicket()
        {

            ListIncident listIncident = new ListIncident();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                listIncident.Reference = Request.QueryString["id"];
                listIncident.Image = meniconHelperEntities.document.First(x => x.id_anomaly == (meniconHelperEntities.incident.FirstOrDefault(y => y.incident_code == listIncident.Reference).id_anomaly)).name;
                listIncident.Description = meniconHelperEntities.incident.First(x => x.incident_code == listIncident.Reference).description;
                listIncident.Supervisor = "?";
                listIncident.Date = meniconHelperEntities.incident.First(x => x.incident_code == listIncident.Reference).date_create;
                listIncident.Declarant = meniconHelperEntities.incident.First(x => x.incident_code == listIncident.Reference).person.first_name + " " + meniconHelperEntities.incident.First(x => x.incident_code == listIncident.Reference).person.last_name; ;
                listIncident.Type = meniconHelperEntities.incident.First(x => x.incident_code == listIncident.Reference).statut.label;
                listIncident.Area = meniconHelperEntities.incident.First(x => x.incident_code == listIncident.Reference).area.name;
                listIncident.Engine = meniconHelperEntities.incident.First(x => x.incident_code == listIncident.Reference).engine.name;
            }

            return listIncident;

        }
    }
}