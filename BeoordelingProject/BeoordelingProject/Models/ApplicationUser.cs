﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public virtual List<StudentRollen> StudentRollen { get; set; }
        public bool MailZenden { get; set; }
    }
}