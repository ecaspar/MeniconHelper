using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MeniconHelper.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            //Someone who isn't logged can't access to this view.
            if (Session["User"] != null)
            {
                LoadBag();
                return View();
            }
            else
                return View("../Login/Index");
        }

        [HttpPost]
        public ActionResult AddRole(Models.ListModel role)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<string> list = new List<string>();
                foreach(var r in meniconHelperEntities.role)
                {
                    list.Add(r.label);
                }
                if(role.Roles.label != null && !list.Contains(role.Roles.label))
                {
                    meniconHelperEntities.role.Add(role.Roles);
                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult AddUser(Models.ListModel person)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<string> list = new List<string>();
                foreach (var p in meniconHelperEntities.person)
                {
                    list.Add(p.email);
                    list.Add(p.username);
                    list.Add(p.phone);
                }
                if (!list.Contains(person.Persons.username))
                {
                    person.Persons.password_scan = person.Persons.username;
                    if(person.Persons.email != null && person.Persons.username != null && person.Persons.first_name != null && person.Persons.last_name != null && person.Persons.password_default != null && person.Persons.phone != null && person.Persons.password_scan != null)
                    {
                        person.Persons.language = "Fr";
                        person.Persons.password_default = GenerateMD5(person.Persons.password_default);
                        meniconHelperEntities.person.Add(person.Persons);
                        meniconHelperEntities.SaveChanges();
                    }
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult AddArea(Models.ListModel area)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<string> list = new List<string>();
                foreach (var a in meniconHelperEntities.area)
                {
                    list.Add(a.name);
                }
                if (area.Areas.name != null && !list.Contains(area.Areas.name))
                {
                    area.Areas.date_create = DateTime.Now;
                    meniconHelperEntities.area.Add(area.Areas);
                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult AddEngine(Models.ListModel engine)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<string> list = new List<string>();
                foreach (var e in meniconHelperEntities.engine)
                {
                    list.Add(e.name);
                    list.Add(e.serial_number);
                }
                if (engine.Engines.name != null && engine.Engines.serial_number != null && !list.Contains(engine.Engines.name) && !list.Contains(engine.Engines.serial_number))
                {
                    engine.Engines.date_create = DateTime.Now;
                    meniconHelperEntities.engine.Add(engine.Engines);
                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult AddTypeIncident(Models.ListModel type_Incident)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<string> list = new List<string>();
                foreach (var i in meniconHelperEntities.type_incident)
                {
                    list.Add(i.label);
                }
                if (type_Incident.TypeIncidents.label != null && !list.Contains(type_Incident.TypeIncidents.label))
                {
                    meniconHelperEntities.type_incident.Add(type_Incident.TypeIncidents);
                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult DisplayRole(Models.ListModel role)
        {
            LoadBag();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                role roles = meniconHelperEntities.role.First(i => i.id_role == role.Roles.id_role);

                ViewBag.ChangeRole = roles;

                Session["ChangeRole"] = roles;

                return View("../Admin/Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateRole(Models.ListModel role)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                role CurrentRole = (role)Session["ChangeRole"];

                List<string> list = new List<string>();
                foreach (var r in meniconHelperEntities.role)
                {
                    list.Add(r.label);
                }
                if (role.Roles.label != null && !list.Contains(role.Roles.label))
                {
                    role r = meniconHelperEntities.role.Single(x => x.id_role == CurrentRole.id_role);
                    r.label = role.Roles.label;
                    meniconHelperEntities.SaveChanges();
                }
                Session["ChangeRole"] = null;
            }

            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult DisplayUser(Models.ListModel person)
        {
            LoadBag();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                person persons = meniconHelperEntities.person.First(i => i.id_person == person.Persons.id_person);

                ViewBag.ChangePerson = persons;

                Session["ChangePerson"] = persons;

                return View("../Admin/Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateUser(Models.ListModel person)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                person CurrentPerson = (person)Session["ChangePerson"];

                List<string> list = new List<string>();
                foreach (var p in meniconHelperEntities.person)
                {
                    list.Add(p.username);
                }
                if (!list.Contains(person.Persons.username) || CurrentPerson.username == person.Persons.username)
                {
                    if (person.Persons.email != null && person.Persons.username != null && person.Persons.first_name != null && person.Persons.last_name != null && person.Persons.phone != null)
                    {
                        person p = meniconHelperEntities.person.Single(x => x.id_person == CurrentPerson.id_person);
                        p.email = person.Persons.email;
                        p.username = person.Persons.username;
                        p.first_name = person.Persons.first_name;
                        p.last_name = person.Persons.last_name;
                        p.phone = person.Persons.phone;
                        meniconHelperEntities.SaveChanges();
                    }
                }
            }
            Session["ChangePerson"] = null;
            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult DisplayArea(Models.ListModel area)
        {
            LoadBag();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                area areas = meniconHelperEntities.area.First(i => i.id_area == area.Areas.id_area);

                ViewBag.ChangeArea = areas;

                Session["ChangeArea"] = areas;

                return View("../Admin/Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateArea(Models.ListModel area)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                area CurrentArea = (area)Session["ChangeArea"];

                List<string> list = new List<string>();
                foreach (var a in meniconHelperEntities.area)
                {
                    list.Add(a.name);
                }
                if (area.Areas.name != null && !list.Contains(area.Areas.name))
                {
                    area a = meniconHelperEntities.area.Single(x => x.id_area == CurrentArea.id_area);
                    a.name = area.Areas.name;
                    meniconHelperEntities.SaveChanges();
                }
            }
            Session["ChangeArea"] = null;
            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult DisplayEngine(Models.ListModel engine)
        {
            LoadBag();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                engine engines = meniconHelperEntities.engine.First(i => i.id_engine == engine.Engines.id_engine);

                ViewBag.ChangeEngine = engines;

                Session["ChangeEngine"] = engines;

                return View("../Admin/Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateEngine(Models.ListModel engine)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {

                engine CurrentEngine = (engine)Session["ChangeEngine"];

                List<string> list = new List<string>();
                foreach (var e in meniconHelperEntities.engine)
                {
                    list.Add(e.serial_number);
                }
                if (!list.Contains(engine.Engines.serial_number) || engine.Engines.serial_number == CurrentEngine.serial_number)
                {
                    engine e = meniconHelperEntities.engine.Single(x => x.id_engine == CurrentEngine.id_engine);
                    e.name = engine.Engines.name;
                    e.serial_number = engine.Engines.serial_number;
                    meniconHelperEntities.SaveChanges();
                }
            }
            Session["ChangeEngine"] = null;
            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult DisplayTypeIncident(Models.ListModel type_Incident)
        {
            LoadBag();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                type_incident type_incidents = meniconHelperEntities.type_incident.First(i => i.id_type_anomaly == type_Incident.TypeIncidents.id_type_anomaly);

                ViewBag.ChangeTypeIncident = type_incidents;

                Session["ChangeTypeIncident"] = type_incidents;

                return View("../Admin/Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateTypeIncident(Models.ListModel type_Incident)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                type_incident CurrentTypeIncident = (type_incident)Session["ChangeTypeIncident"];

                List<string> list = new List<string>();
                foreach (var i in meniconHelperEntities.type_incident)
                {
                    list.Add(i.label);
                }
                if (type_Incident.TypeIncidents.label != null && !list.Contains(type_Incident.TypeIncidents.label))
                {
                    type_incident i = meniconHelperEntities.type_incident.Single(x => x.label == CurrentTypeIncident.label);
                    i.label = type_Incident.TypeIncidents.label;
                    meniconHelperEntities.SaveChanges();
                }
            }
            Session["ChangeTypeIncident"] = null;
            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult DisplayLinkedIncident(Models.ListModel type_Incident)
        {
            LoadBag();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                type_incident type_incidents = meniconHelperEntities.type_incident.First(i => i.id_type_anomaly == type_Incident.TypeIncidents.id_type_anomaly);

                ViewBag.LinkedTypeIncident = type_incidents;

                List<role> listRole = new List<role>();
                foreach (var r in meniconHelperEntities.role)
                {
                    if(!type_incidents.role.Contains(r))
                    {
                        listRole.Add(r);
                    }

                }
                ViewBag.roles = listRole;

                Session["LinkedTypeIncident"] = type_incidents;

                return View("../Admin/Index");
            }
        }

        [HttpPost]
        public ActionResult LinkRole(Models.ListModel role)
        {

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                type_incident CurrentTypeIncident = (type_incident)Session["LinkedTypeIncident"];

                role r = meniconHelperEntities.role.Single(x => x.id_role == role.Roles.id_role);
                type_incident  t  = meniconHelperEntities.type_incident.Single(x => x.id_type_anomaly == CurrentTypeIncident.id_type_anomaly);
                t.role.Add(r);

                meniconHelperEntities.SaveChanges();
            }

            return RedirectToAction("../Admin/Index");
        }

        [HttpPost]
        public ActionResult DisplayLinkedIncidentRemove(Models.ListModel type_Incident)
        {
            LoadBag();

            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                type_incident type_incidents = meniconHelperEntities.type_incident.First(i => i.id_type_anomaly == type_Incident.TypeIncidents.id_type_anomaly);

                ViewBag.LinkedTypeIncidentRemove = type_incidents;

                List<role> listRole = new List<role>();
                foreach (var r in meniconHelperEntities.role)
                {
                    if (type_incidents.role.Contains(r))
                    {
                        listRole.Add(r);
                    }
                }
                ViewBag.roles = listRole;

                Session["LinkedTypeIncident"] = type_incidents;

                return View("../Admin/Index");
            }
        }

        [HttpPost]
        public ActionResult UnlinkRole(Models.ListModel role)
        {


            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                type_incident CurrentTypeIncident = (type_incident)Session["LinkedTypeIncident"];

                role r = meniconHelperEntities.role.Single(x => x.id_role == role.Roles.id_role);
                type_incident t = meniconHelperEntities.type_incident.Single(x => x.id_type_anomaly == CurrentTypeIncident.id_type_anomaly);
                if(t.role.Contains(r))
                    t.role.Remove(r);

                meniconHelperEntities.SaveChanges();
            }

            return RedirectToAction("../Admin/Index");
        }

        public string GenerateMD5(string rawString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(rawString)).Select(s => s.ToString("x2")));
        }

        public void LoadBag()
        {
            //Get which user is logged.
            person p = (person)(Session["User"]);
            ViewBag.name = p.first_name + " " + p.last_name;
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<role> listRole = new List<role>();
                foreach (var r in meniconHelperEntities.role)
                {
                    listRole.Add(r);
                }
                ViewBag.roles = listRole;
                List<person> listPerson = new List<person>();
                foreach (var pe in meniconHelperEntities.person)
                {
                    listPerson.Add(pe);
                }
                ViewBag.persons = listPerson;
                List<area> listArea = new List<area>();
                foreach (var ar in meniconHelperEntities.area)
                {
                    listArea.Add(ar);
                }
                ViewBag.areas = listArea;
                List<engine> listEngine = new List<engine>();
                foreach (var en in meniconHelperEntities.engine)
                {
                    listEngine.Add(en);
                }
                ViewBag.engines = listEngine;
                List<type_incident> listTypeIncident = new List<type_incident>();
                foreach (var ti in meniconHelperEntities.type_incident)
                {
                    listTypeIncident.Add(ti);
                }
                ViewBag.type_incidents = listTypeIncident;
            }

            if (p.id_role == 7)
                ViewBag.Admin = true;
            else
                ViewBag.Admin = false;

        }
    }

}