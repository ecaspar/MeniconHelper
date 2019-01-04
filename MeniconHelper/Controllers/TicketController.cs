using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeniconHelper.Models;
using MeniconHelper.App_LocalResources;

namespace MeniconHelper.Controllers
{
    /// <summary>
    /// Ticket Controller link to the view Views/Ticket/Index
    /// 
    /// Display the ticket selected by the user.
    /// </summary>
    public class TicketController : Controller
    {
        //Main view of the controller.
        public ActionResult Index()
        {
            //Someone who isn't logged can't access to this view.
            if (Session["User"] != null)
            {
                //Get which user is logged.
                person p = (person)(Session["User"]);
                ViewBag.name = p.first_name + " " + p.last_name;

                //Call the function which load the ticket and all the task/comment from this ticket.
                ViewBag.Ticket = LoadTicket();
                ViewBag.Task = LoadTask();

                //Get the reference of the ticket choose using the URL
                string code = Request.QueryString["id"];
                Session["Ticket"] = code;
                
                //Connect to the db using EntityFramework  - MeniconHelperBDD.edmx to get the diagram.
                using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
                {
                    //Get the incident which correspond to the reference
                    incident i = meniconHelperEntities.incident.Where(x => x.incident_code == code).First();
                    if(i.date_create == i.date_processing)
                    {
                        i.date_processing = DateTime.Now;
                        i.id_statut = 3;
                        meniconHelperEntities.SaveChanges();
                    }
                    List<int> listPerson = new List<int>();
                    /*
                    //Allow the logged user to add a task/comment only if he's supervisor/creator of the ticket
                    type_incident type = meniconHelperEntities.type_incident.Where(x => x.id_type_anomaly == i.id_type_anomaly).First();
                    foreach (var r in type.role)
                    {
                        if(r.id_role!=0)
                        {
                            foreach (var per in r.person)
                            {
                                listPerson.Add(per.id_person);
                            }
                        }
                    }
                    if (i.person.id_person == p.id_person || listPerson.Contains(p.id_person))
                        ViewBag.Authorize = true;
                    else
                        ViewBag.Authorize = false;
                    //
                    */
                    if (p.id_role == 0)
                        ViewBag.Admin = true;
                    else
                        ViewBag.Admin = false;
                }
                return View();
            }
            else
                return View("../Login/Index");
        }

        //Function that add a task/comment in the db.
        [HttpPost]
        public ActionResult AddComment(task newTask)
        {

            //Get the reference of the ticket.
            string code = Session["Ticket"].ToString();

            //If the comment field isn't empty.
            if (newTask.comment != null)
            {

                List<int> listPerson = new List<int>();
                person p = (person)(Session["User"]);

                //Connect to the db using EntityFramework  - MeniconHelperBDD.edmx to get the diagram.
                using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
                {
                    if (meniconHelperEntities.Database.Exists())
                    {
                        //Get the incident which correspond to the reference
                        incident i = meniconHelperEntities.incident.Where(x => x.incident_code == code).First();

                        type_incident type = meniconHelperEntities.type_incident.Where(x => x.id_type_anomaly == i.id_type_anomaly).First();
                        foreach (var r in type.role)
                        {
                            if (r.id_role != 0)
                            {
                                foreach (var per in r.person)
                                {
                                    listPerson.Add(per.id_person);
                                }
                            }
                        }
                        //Second check if the user can add a task/comment.
                        //if (i.person.id_person == p.id_person || listPerson.Contains(p.id_person))
                        //{
                        newTask.date_create = DateTime.Now;
                        newTask.id_person = p.id_person;
                        newTask.id_anomaly = i.id_anomaly;
                        newTask.date_close = DateTime.Now;

                        meniconHelperEntities.task.Add(newTask);
                        i.date_update = DateTime.Now;

                        meniconHelperEntities.SaveChanges();
                        //}
                    }
                    else
                        ViewBag.Message = GlobalRes.dbOffline;
                }

            }

            return RedirectToAction("../Ticket/Index", new { id = code });

        }

        [HttpPost]
        public ActionResult CloseTicket()
        {
            string code = Session["Ticket"].ToString();
            //Connect to the db using EntityFramework  - MeniconHelperBDD.edmx to get the diagram.
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                if (meniconHelperEntities.Database.Exists())
                {
                    //Get the incident which correspond to the reference
                    incident i = meniconHelperEntities.incident.Where(x => x.incident_code == code).First();
                    i.date_close = DateTime.Now;
                    i.id_statut = 4;

                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Ticket/Index", new { id = code });
        }

        //Load the ticket selected by the user
        private ListIncident LoadTicket()
        {

            ListIncident listIncident = new ListIncident();

            //Connect to the db using EntityFramework  - MeniconHelperBDD.edmx to get the diagram.
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                if (meniconHelperEntities.Database.Exists())
                {
                    //Get the reference of the ticket.
                    string code = Request.QueryString["id"];

                    //Get the incident which correspond to the reference
                    incident i = meniconHelperEntities.incident.Where(x => x.incident_code == code).First();
                    List<document> images = new List<document>();

                    foreach (var image in i.document)
                    {
                        images.Add(image);
                    }
                    List<person> listPerson = new List<person>();
                    type_incident type = meniconHelperEntities.type_incident.Where(x => x.id_type_anomaly == i.id_type_anomaly).First();
                    foreach (var r in type.role)
                    {
                        if (r.id_role != 0)
                        {
                            foreach (var p in r.person)
                            {
                                listPerson.Add(p);
                            }
                        }
                    }

                    listIncident.Reference = i.incident_code;
                    listIncident.Image = images;
                    listIncident.Description = i.description;
                    listIncident.Supervisor = listPerson;
                    listIncident.Date = i.date_create;
                    listIncident.Declarant = i.person.first_name + " " + i.person.last_name;
                    listIncident.Type = i.type_incident.label;
                    listIncident.Status = i.statut.label;
                    listIncident.Area = i.area.name;
                    listIncident.Engine = i.engine.name;
                }
                else
                    return ViewBag.Message = GlobalRes.dbOffline;
            }

            return listIncident;

        }

        //Load all the tasks/comments linked to the ticket selected by the user.
        private List<ListTask> LoadTask()
        {

            List<ListTask> list = new List<ListTask>();

            //Connect to the db using EntityFramework  - MeniconHelperBDD.edmx to get the diagram.
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                if (meniconHelperEntities.Database.Exists())
                {
                    //Get the reference of the ticket.
                    string reference = Request.QueryString["id"];
                    //Get the incident which correspond to the reference
                    int idTicket = meniconHelperEntities.incident.First(x => x.incident_code == reference).id_anomaly;

                    //Load all the tasks/comments from the db linked to the id_anomaly
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
                else
                    ViewBag.Message = GlobalRes.dbOffline;

            }

            return list;

        }

    }
}