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

                string code = Request.QueryString["id"];
                Session["Ticket"] = code;
                using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
                {
                    incident i = meniconHelperEntities.incident.Where(x => x.incident_code == code).First();
                    List<int> listPerson = new List<int>();
                    type_incident type = meniconHelperEntities.type_incident.Where(x => x.id_type_anomaly == i.id_type_anomaly).First();
                    foreach (var per in type.person)
                    {
                        listPerson.Add(per.id_person);
                    }
                    if (i.person.id_person == p.id_person || listPerson.Contains(p.id_person))
                        ViewBag.Authorize = true;
                    else
                        ViewBag.Authorize = false;
                }
                return View();
            }
            else
                return View("../Login/Index");
        }

        [HttpPost]
        public ActionResult AddComment(task newTask)
        {

            string code = Session["Ticket"].ToString();
            List<int> listPerson = new List<int>();
            person p = (person)(Session["User"]);

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                incident i = meniconHelperEntities.incident.Where(x => x.incident_code == code).First();

                type_incident type = meniconHelperEntities.type_incident.Where(x => x.id_type_anomaly == i.id_type_anomaly).First();
                foreach (var per in type.person)
                {
                    listPerson.Add(per.id_person);
                }
                if (i.person.id_person == p.id_person || listPerson.Contains(p.id_person))
                {
                    newTask.date_create = DateTime.Now;
                    newTask.id_person = p.id_person;
                    newTask.id_anomaly = i.id_anomaly;
                    newTask.date_close = DateTime.Now;

                    meniconHelperEntities.task.Add(newTask);

                    meniconHelperEntities.SaveChanges();
                }
            }

            return RedirectToAction("../Ticket/Index", new { id = code });

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