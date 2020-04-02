﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using Faculty.Mappers;
using Faculty.Models;

namespace Faculty.Controllers
{
    public class GradebookController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IThemeService _themeService;

        public GradebookController(ICourseService courseService, IUserService userService, IThemeService themeService)
        {
            _courseService = courseService;
            _userService = userService;
            _themeService = themeService;
        }
        [HttpGet]
        [Authorize]
        public ActionResult List(int courseId)
        {
            var gradebook = _courseService.GetGradebookForCourse(courseId);
            
            //var vb = ViewBag;
            //var students = _userService.GetStudentsByCourse(courseId);
            //List<GradeViewModel> grades = new List<GradeViewModel>();
            
            //foreach (var student in students)
            //{
            //    if (!gradebook.Where(x => x.Key.id == student.id).Any())
            //    {
            //        gradebook.Add(student, null);
            //    }
            //}

            
            
            //var viewgrade = new Dictionary<UserView, int>();
            //var keys = gradebook.Keys.Select(k => k.Map()).ToList();
            //var values = gradebook.Values.ToList();
            //var viewGradebook = new GradebookViewModel(keys,values);
            //var dic = keys.Select((k, i) => new { k, v = values[i] })
             //   .ToDictionary(x => x.k, x => x.v);
            
            var a = gradebook;
            List<GradeViewModel> grades= new List<GradeViewModel>();
            gradebook.ForEach(x=>grades.Add(new GradeViewModel(x.Student.Name,x.Grade)));
            IList <GradeViewModel > igrades = grades;
            return View(igrades);
        }

        [HttpPost]
        public ActionResult Save(IList<GradeViewModel> igrades)
        {
            var d = igrades;
            return RedirectToAction("List");
        }
    }
}