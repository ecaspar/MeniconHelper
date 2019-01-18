using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MeniconHelper.Models;
using MeniconHelper.App_LocalResources;
using System.Drawing;

namespace MeniconHelper.Controllers
{
    /// <summary>
    /// Home Controller link to the view Views/Home/Index
    /// 
    /// Display all the ticket in the base using the function "LoadIncident".
    /// </summary>
    public class HomeController : Controller
    {
        //Main view of the controller
        public ActionResult Index()
        {
            //Someone who isn't logged can't access to this view.
            if (Session["User"]!=null)
            {
                //Get which user is logged.
                person p = (person)(Session["User"]);
                ViewBag.name = p.first_name + " " + p.last_name;
                //Call the function which load all ticket. 
                ViewBag.Incident = LoadIncident();

                if (p.id_role == 7)
                    ViewBag.Admin = true;
                else
                    ViewBag.Admin = false;

                return View();
            }
            else
                return View("../Login/Index");
        }

        //Controller for the view Views/Home/MyTickets
        public ActionResult MyTickets()
        {
            //Someone who isn't logged can't access to this view.
            if (Session["User"] != null)
            {
                person p = (person)(Session["User"]);
                ViewBag.name = p.first_name + " " + p.last_name;

                ViewBag.SuperviseIncident = LoadSuperviseIncident();

                if (p.id_role ==7)
                    ViewBag.Admin = true;
                else
                    ViewBag.Admin = false;
                return View();
            }
            else
                return View("../Login/Index");
        }

        //Controller for the view Views/Home/MyDeclarations
        public ActionResult MyDeclarations()
        {
            //Someone who isn't logged can't access to this view.
            if (Session["User"] != null)
            {
                person p = (person)(Session["User"]);
                ViewBag.name = p.first_name + " " + p.last_name;

                ViewBag.DeclarantIncident = LoadDeclarantIncident();

                if (p.id_role == 7)
                    ViewBag.Admin = true;
                else
                    ViewBag.Admin = false;

                return View();
            }
            else
                return View("../Login/Index");
        }

        //Redirect in the view Views/Ticket/Index.
        public ActionResult Select(string reference)
        {
            return RedirectToAction("../Ticket/Index", new { id = reference });
        }

        //Sign out function
        public ActionResult SignOut()
        {
            Session["User"] = null;
            return View("../Login/Index");
        }

        public ActionResult Admin()
        {
            return RedirectToAction("../Admin/Index");
        }

        //Load All tickets from the db
        private List<ListIncident> LoadIncident()
        {
            //Create a list of Incident which is a Model - Models/ListIncident
            List<ListIncident> list = new List<ListIncident>();

            //Connect to the db using EntityFramework.
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                if (meniconHelperEntities.Database.Exists())
                {
                    //List all incident in the db
                    foreach (incident i in meniconHelperEntities.incident)
                    {
                        ListIncident listIncident = new ListIncident();

                        //List of URL. Each incident can have up to 3 images.
                        List<document> images = new List<document>();
                        if(i.document.Count != 0)
                        {
                            images.Add(i.document.First());
                        }

                        //This part list all the supervisor of this incident.
                        List<person> listPerson = new List<person>();

                        type_incident type = meniconHelperEntities.type_incident.Where(x => x.id_type_anomaly == i.id_type_anomaly).First();
                        foreach (var r in type.role)
                        {
                            if (r.id_role != 7)
                            {
                                foreach (var p in r.person)
                                {
                                    listPerson.Add(p);
                                }
                            }
                        }
                        //

                        listIncident.Reference = i.incident_code;
                        if (i.document.Count != 0)
                            listIncident.Image = images;
                        listIncident.Description = i.description;
                        listIncident.Supervisor = listPerson;
                        listIncident.Date = i.date_create;
                        listIncident.Declarant = i.person.first_name + " " + i.person.last_name;
                        listIncident.Type = i.type_incident.label;
                        listIncident.Status = i.statut.label;
                        listIncident.Area = i.area.name;
                        listIncident.Engine = i.engine.name;
                        listIncident.Criticity = i.criticity.name;

                        list.Add(listIncident);
                    }
                }
                else
                    ViewBag.Message = GlobalRes.dbOffline;
            } 
            return list;
                
        }

        //Load only supervised tickets by the logged user
        private List<ListIncident> LoadSuperviseIncident()
        {
            //Create a list of Incident which is a Model - Models/ListIncident
            List<ListIncident> list = new List<ListIncident>();

            //Connect to the db using EntityFramework - MeniconHelperBDD.edmx to get the diagram.
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                if (meniconHelperEntities.Database.Exists())
                {
                    //List all incident in the db
                    foreach (incident i in meniconHelperEntities.incident)
                    {
                        ListIncident listIncident = new ListIncident();
                        person p = (person)(Session["User"]);
                        List<int> listId = new List<int>();

                        //List of URL. Each incident can have up to 3 images.
                        List<document> images = new List<document>();
                        if (i.document.Count != 0)
                        {
                            images.Add(i.document.First());
                        }

                        //This part list all the supervisor of this incident.
                        List<person> listPerson = new List<person>();
                        type_incident type = meniconHelperEntities.type_incident.Where(x => x.id_type_anomaly == i.id_type_anomaly).First();
                        foreach (var r in type.role)
                        {
                            if (r.id_role != 7)
                            {
                                foreach (var per in r.person)
                                {
                                    listPerson.Add(per);
                                    listId.Add(per.id_person);
                                }
                            }
                        }
                        //

                        //Only add to the list of ticket if the user supervise the ticket
                        if (listId.Contains(p.id_person))
                        {
                            listIncident.Reference = i.incident_code;
                            if (i.document.Count != 0)
                                listIncident.Image = images;
                            listIncident.Description = i.description;
                            listIncident.Supervisor = listPerson;
                            listIncident.Date = i.date_create;
                            listIncident.Declarant = i.person.first_name + " " + i.person.last_name;
                            listIncident.Type = i.type_incident.label;
                            listIncident.Status = i.statut.label;
                            listIncident.Area = i.area.name;
                            listIncident.Engine = i.engine.name;

                            list.Add(listIncident);
                        }
                    }
                }
                else
                    ViewBag.Message = GlobalRes.dbOffline;
            }
            return list;

        }

        //Load only created tickets by the logged user
        private List<ListIncident> LoadDeclarantIncident()
        {
            //Create a list of Incident which is a Model - Models/ListIncident
            List<ListIncident> list = new List<ListIncident>();
            //Connect to the bdd using EntityFramework - MeniconHelperBDD.edmx to get the diagram.
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                if (meniconHelperEntities.Database.Exists())
                {
                    //List all incident in the db
                    foreach (incident i in meniconHelperEntities.incident)
                    {
                        ListIncident listIncident = new ListIncident();
                        person p = (person)(Session["User"]);

                        //List of URL. Each incident can have up to 3 images.
                        List<document> images = new List<document>();
                        if (i.document.Count != 0)
                        {
                            images.Add(i.document.First());
                        }
                            

                        //This part list all the supervisor of this incident.
                        List<person> listPerson = new List<person>();
                        type_incident type = meniconHelperEntities.type_incident.Where(x => x.id_type_anomaly == i.id_type_anomaly).First();
                        foreach (var r in type.role)
                        {
                            if (r.id_role != 7)
                            {
                                foreach (var per in r.person)
                                {
                                    listPerson.Add(per);
                                }
                            }
                        }
                        //

                        //Only add the ticket if the user created the ticket.
                        if (i.person.id_person == p.id_person)
                        {
                            listIncident.Reference = i.incident_code;
                            if (i.document.Count != 0)
                                listIncident.Image = images;
                            listIncident.Description = i.description;
                            listIncident.Supervisor = listPerson;
                            listIncident.Date = i.date_create;
                            listIncident.Declarant = i.person.first_name + " " + i.person.last_name;
                            listIncident.Type = i.type_incident.label;
                            listIncident.Status = i.statut.label;
                            listIncident.Area = i.area.name;
                            listIncident.Engine = i.engine.name;

                            list.Add(listIncident);
                        }
                    }
                }
                else
                    ViewBag.Message = GlobalRes.dbOffline;
            }
            return list;
        }
    }
}