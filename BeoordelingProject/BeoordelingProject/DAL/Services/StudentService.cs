﻿using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Services
{
    public class StudentService : BeoordelingProject.DAL.Services.IStudentService
    {
        IUnitOfWork uow = null;
        IStudentRepository studentRepository = null;
        IAccountRepository accountRepository = null;
        IGenericRepository<Rol> rolRepository = null;

        public StudentService()
        {

        }

        public StudentService(IUnitOfWork uow, IStudentRepository studentRepository, IAccountRepository accountRepository, IGenericRepository<Rol> rolRepository)
        {
            this.uow = uow;
            this.studentRepository = studentRepository;
            this.accountRepository = accountRepository;
            this.rolRepository = rolRepository;
        }

        public List<Student> GetStudenten()
        {
            return studentRepository.All().ToList<Student>();
        }

        public List<Student> CreateStudenten(string csvData)
        {
            List<Student> studenten = new List<Student>();

            using (StringReader textReader = new StringReader(csvData))
            {
                string line = textReader.ReadLine();
                int skipCount = 0;

                while (line != null && skipCount < 1)
                {
                    line = textReader.ReadLine();

                    skipCount++;
                }

                while (line != null)
                {
                    string[] columns = line.Split(';');
                    Student student = new Student { Naam = columns[7], Trajecttype = columns[20], Opleiding = columns[1], Email = columns[15], StudentId = Int32.Parse(columns[14]), Geslacht = columns[18], Geboortedatum = columns[17] };

                    studentRepository.Insert(student);
                    uow.SaveChanges();

                    studenten.Add(student);

                    line = textReader.ReadLine();
                }
            }
            
            return studenten;
        }

        public List<String> GetOpleidingen()
        {
            return studentRepository.GetOpleidingen().ToList<String>();
        }

        public List<ApplicationUser> GetUsers()
        {
            return accountRepository.All().ToList();
        }

        public void DeleteUser(ApplicationUser user)
        {
            accountRepository.DeleteGebruiker(user);

        }

        public List<Rol> GetRoles()
        {
            return studentRepository.GetRoles().ToList();
        }

        public IHtmlString SerializeObject(object value, object otherValue) {
            List<Student> studenten = (List<Student>)value;
            List<List<Rol>> studentPerRollen = (List<List<Rol>>)otherValue;

            string jsonString = "{Studenten:[";

            for (int i = 0; i < studenten.Count; i++) {
                for (int j = 0; j < studentPerRollen[i].Count; j++) {
                    jsonString += "{";
                    jsonString += "studentRol: \"" + studenten[i].ID + "." + studentPerRollen[i][j].ID + "\", ";
                    jsonString += "naam: \"" + studenten[i].Naam + "\", ";
                    jsonString += "opleiding: \"" + studenten[i].Opleiding + "\", ";
                    jsonString += "rol: \"" + studentPerRollen[i][j].Naam + "\"";
                    jsonString += "},";
                }
            }

            //laatste komma wissen, deze is niet nodig
            jsonString = jsonString.Remove(jsonString.Length - 1);

            jsonString += "]}";

            return new HtmlString(jsonString);
        }

        public IHtmlString SerializeObject(object value)
        {
            List<Student> studenten = (List<Student>)value;
            string jsonString = "{Studenten:[";
            foreach (Student student in studenten)
            {
                jsonString += "{";
                jsonString+="id: "+ student.ID +",";
                jsonString += "naam: \"" + student.Naam + "\",";
                jsonString += "trajecttype: \"" + student.Trajecttype + "\",";
                jsonString += "opleiding: \"" + student.Opleiding + "\",";
                jsonString += "email: \"" + student.Email + "\",";
                jsonString += "studentId: " + student.StudentId + ",";
                jsonString += "geslacht: \"" + student.Geslacht + "\",";
                jsonString += "geboortedatum: \"" + student.Geboortedatum + "\"";
                jsonString += "},";
            }
            //laatste komma wissen, deze is niet nodig
            jsonString = jsonString.Remove(jsonString.Length - 1);

            jsonString += "]}";

            return new HtmlString(jsonString);
        }

        public Student GetStudentByID(int id)
        {
            return studentRepository.GetByID(id);
        }

        public Rol GetRolById(int id)
        {
            return rolRepository.GetByID(id);
        }

        public List<Student> GetStudentenByStudentRollen(List<StudentRollen> studentRollen) {
            List<Student> studenten = new List<Student>();
            
            foreach (StudentRollen studentRol in studentRollen) {
                studenten.Add(studentRol.Student);
            }

            return studenten;
        }

        public List<List<Rol>> GetRollenByStudent(List<StudentRollen> studentRollen) {
            List<List<Rol>> rollen = new List<List<Rol>>();

            for (int i = 0; i < studentRollen.Count; i++) {
                rollen.Add(studentRollen[i].Rollen);
            }

            return rollen;
        }

        public int GetAantalTeTonenStudenten(List<StudentRollen> studentRollen) {
            int counter = 0;

            for (int i = 0; i < GetStudentenByStudentRollen(studentRollen).Count; i++) {
                for (int j = 0; j < GetRollenByStudent(studentRollen)[i].Count; j++) {
                    counter++;
                }
            }

            return counter;
        }

        public ApplicationUser GetUserById(string userId)
        {
            return accountRepository.GetByID(userId);
        }

    }
}