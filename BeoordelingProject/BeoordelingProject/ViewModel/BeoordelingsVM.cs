﻿using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.ViewModel {
    public class BeoordelingsVM {
        public Resultaat Resultaten { get; set; }
        public Matrix Matrix { get; set; }
        public Student Student { get; set; }
        public int Rol_ID { get; set; }
    }
}