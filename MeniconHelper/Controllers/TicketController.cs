﻿using System;
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
                string code = Request.QueryString["id"];
                incident i = meniconHelperEntities.incident.Where(x=>x.incident_code == code).First();
                List<string> images = new List<string>();

                foreach(var image in i.document)
                {
                    images.Add(image.name);
                }

                listIncident.Reference = i.incident_code;
                listIncident.Image = images;
                listIncident.Description = i.description;
                listIncident.Supervisor = "?";
                listIncident.Date = i.date_create;
                listIncident.Declarant = i.person.first_name + " " +i.person.last_name; ;
                listIncident.Type = i.statut.label;
                listIncident.Area = i.area.name;
                listIncident.Engine = i.engine.name;
            }

            return listIncident;

        }
    }
}