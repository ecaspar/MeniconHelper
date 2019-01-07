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
                }

                if (p.id_role == 0)
                    ViewBag.Admin = true;
                else
                    ViewBag.Admin = false;

                return View();
            }
            else
                return View("../Login/Index");
        }

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
                if (!list.Contains(person.Persons.username) && !list.Contains(person.Persons.email) && !list.Contains(person.Persons.phone))
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
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                role roles = meniconHelperEntities.role.First(i => i.id_role == role.Roles.id_role);

                person p = (person)(Session["User"]);
                if (p.id_role == 0)
                    ViewBag.Admin = true;
                else
                    ViewBag.Admin = false;
                List<role> listRole = new List<role>();
                foreach (var r in meniconHelperEntities.role)
                {
                    listRole.Add(r);
                }
                ViewBag.roles = listRole;

                ViewBag.ChangeRole = roles;

                return View("../Admin/Index");
            }
        }

        public ActionResult UpdateRole(role role)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<string> list = new List<string>();
                foreach (var r in meniconHelperEntities.role)
                {
                    list.Add(r.label);
                }
                if (role.label != null && !list.Contains(role.label))
                {
                    role r = meniconHelperEntities.role.Single(x => x.label == role.label);
                    r.label = role.label;
                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        public ActionResult UpdateUser(person person)
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
                if (!list.Contains(person.username) && !list.Contains(person.email) && !list.Contains(person.phone))
                {
                    if (person.email != null && person.username != null && person.first_name != null && person.last_name != null && person.password_default != null && person.phone != null && person.password_scan != null)
                    {
                        person p = meniconHelperEntities.person.Single(x => x.username == person.username);
                        person.password_default = GenerateMD5(person.password_default);
                        p.email = person.email;
                        p.username = person.username;
                        p.first_name = person.first_name;
                        p.last_name = person.last_name;
                        p.password_default = person.password_default;
                        p.phone = person.password_default;
                        meniconHelperEntities.SaveChanges();
                    }
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        public ActionResult UpdateArea(area area)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<string> list = new List<string>();
                foreach (var a in meniconHelperEntities.area)
                {
                    list.Add(a.name);
                }
                if (area.name != null && !list.Contains(area.name))
                {
                    area a = meniconHelperEntities.area.Single(x => x.name == area.name);
                    a.name = area.name;
                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        public ActionResult UpdateEngine(engine engine)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<string> list = new List<string>();
                foreach (var e in meniconHelperEntities.engine)
                {
                    list.Add(e.name);
                    list.Add(e.serial_number);
                }
                if (engine.name != null && engine.serial_number != null && !list.Contains(engine.name) && !list.Contains(engine.serial_number))
                {
                    engine e = meniconHelperEntities.engine.Single(x => x.name == engine.name);
                    e.name = engine.name;
                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        public ActionResult UpdateTypeIncident(type_incident type_Incident)
        {
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                List<string> list = new List<string>();
                foreach (var i in meniconHelperEntities.type_incident)
                {
                    list.Add(i.label);
                }
                if (type_Incident.label != null && !list.Contains(type_Incident.label))
                {
                    type_incident i = meniconHelperEntities.type_incident.Single(x => x.label == type_Incident.label);
                    i.label = type_Incident.label;
                    meniconHelperEntities.SaveChanges();
                }
            }
            return RedirectToAction("../Admin/Index");
        }

        public string GenerateMD5(string rawString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(rawString)).Select(s => s.ToString("x2")));
        }
    }

}