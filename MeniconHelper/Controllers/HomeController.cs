using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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

                ViewBag.Incident = LoadIncident();

                return View();
            }
            else
                return View("../Login/Index");
        }

        public ActionResult Select(string reference)
        {
            return RedirectToAction("../Ticket/Index", new { id = reference });
        }

        public List<ListIncident> LoadIncident()
        {
            List<ListIncident> list = new List<ListIncident>();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                foreach (incident i in meniconHelperEntities.incident)
                {
                    ListIncident listIncident = new ListIncident();

                    List<string> images = new List<string>();
                    images.Add(i.document.First().name);

                    List<person> listPerson = new List<person>();

                    type_incident type = meniconHelperEntities.type_incident.Where(x=>x.id_type_anomaly==i.id_type_anomaly).First();
                    foreach (var p in type.person)
                    {
                        listPerson.Add(p);
                    }


                    listIncident.Reference = i.incident_code;
                    listIncident.Image = images;
                    listIncident.Description = i.description;
                    listIncident.Supervisor = listPerson;
                    listIncident.Date = i.date_create;
                    listIncident.Declarant = i.person.first_name + " " + i.person.last_name;
                    listIncident.Type = i.statut.label;
                    listIncident.Area = i.area.name;
                    listIncident.Engine = i.engine.name;

                    list.Add(listIncident);
                }
            } 
            return list;
                
        }
    }
}