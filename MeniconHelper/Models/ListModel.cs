﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeniconHelper.Models
{
    public class ListModel
    {
        public MeniconHelper.person Persons { get; set; }
        public MeniconHelper.role Roles { get; set; }
        public MeniconHelper.area Areas { get; set; }
        public MeniconHelper.engine Engines { get; set; }
        public MeniconHelper.type_incident TypeIncidents { get; set; }
    }
}