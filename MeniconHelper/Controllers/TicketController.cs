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
                ViewBag.Task = LoadTask();

                return View();
            }
            else
                return View("../Login/Index");
        }

        private ListIncident LoadTicket()
        {

            ListIncident listIncident = new ListIncident();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                string code = Request.QueryString["id"];
                incident i = meniconHelperEntities.incident.Where(x=>x.incident_code == code).First();
                List<string> images = new List<string>();

                foreach(var image in i.document)
                {
                    images.Add(image.name);
                }
                List<person> listPerson = new List<person>();
                type_incident type = meniconHelperEntities.type_incident.Where(x => x.id_type_anomaly == i.id_type_anomaly).First();
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
            }

            return listIncident;

        }

        private List<ListTask> LoadTask()
        {

            List<ListTask> list = new List<ListTask>();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                string reference = Request.QueryString["id"];

                int idTicket = meniconHelperEntities.incident.First(x => x.incident_code == reference).id_anomaly;

                foreach (task t in meniconHelperEntities.task.Where(x => x.id_anomaly == idTicket))
                {
                    ListTask listTask = new ListTask();

                    listTask.Comment = t.comment;
                    listTask.CreateDate = t.date_create;
                    listTask.CloseDate = t.date_close;
                    listTask.PersonComment = t.person;
                    list.Add(listTask);
                }

            }

            return list;

        }
    }
}