﻿using BeoordelingProject.DAL.Services;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        private IStudentService studentService = null;

        public AdminController()
        {

        }

        public AdminController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public ActionResult Index()
        {
            AdminOverzichtVM vm = new AdminOverzichtVM();
            vm.Studenten = studentService.GetStudenten();
            vm.Opleidingen = studentService.GetOpleidingen();
            vm.StudentenString = studentService.SerializeObject(vm.Studenten);

            return View(vm);
        }

        public ActionResult GetStudent(int id)
        {
            return View(studentService.GetStudentByID(id));
        }

        public ActionResult GeneratePDFStudent(int id)
        {
            return new Rotativa.ActionAsPdf("GetStudent", new { id = id });
        }
	}
}