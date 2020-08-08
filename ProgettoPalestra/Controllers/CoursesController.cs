﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgettoPalestra.Models.Services.Application;
using ProgettoPalestra.Models.ViewModels;

namespace ProgettoPalestra.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Catalogo dei corsi";
            List<CourseViewModel> courses = courseService.GetCourses();
            return View(courses);
        }
        public IActionResult Detail(int id)
        {
            CourseDetailViewModel viewModel = courseService.GetCourse(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}