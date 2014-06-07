﻿using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.ViewModel
{
    public class AccountbeheerVM
    {
        public AccountbeheerVM()
        {

        }

        public SelectList Studenten { get; set; }
        public int SelectedStudentId { get; set; }
        public SelectList Accounts { get; set; }
        public ApplicationUser SelectedAccount { get; set; }
        public ApplicationUser Account { get; set; }
        public List<Rol> Rollen { get; set; }
    }
}