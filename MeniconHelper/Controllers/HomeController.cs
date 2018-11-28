using System;
using System.Collections.Generic;
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

                ViewBag.Incident = LoadIncident();

                return View();
            }
            else
                return View("../Login/Index");
        }


        public List<ListIncident> LoadIncident()
        {
            List<ListIncident> list = new List<ListIncident>();
            

            MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities();
            ListIncident listIncident = new ListIncident();
            /* foreach(incident i in meniconHelperEntities.incident)
            {
                ListIncident listIncident = new ListIncident();

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
                list.Add(listIncident);
            } */

            listIncident.Reference = "TQUA1";
            listIncident.Image = "../Content/Images/Incident.jpg";
            listIncident.Description = "Une description";
            listIncident.Supervisor = "L. Hebinger";
            listIncident.Date = DateTime.Now;
            listIncident.Declarant = "E. Caspar";
            listIncident.Type = "Qualité";
            listIncident.Area = "Ligne 1";
            listIncident.Engine = "Emballage";

            list.Add(listIncident);

            ListIncident listIncident2 = new ListIncident();

            listIncident2.Reference = "TQUA2";
            listIncident2.Image = "../Content/Images/Incident2.jpg";
            listIncident2.Description = "Une description";
            listIncident2.Supervisor = "L. Hebinger";
            listIncident2.Date = DateTime.Now;
            listIncident2.Declarant = "E. Caspar";
            listIncident2.Type = "Qualité";
            listIncident2.Area = "Ligne 1";
            listIncident2.Engine = "Emballage";

            list.Add(listIncident2);

            return list;
        }
    }
}