using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ProgettoPalestra.Models.Services.Infrastructure;
using ProgettoPalestra.Models.ViewModels;

namespace ProgettoPalestra.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;
        public AdoNetCourseService(IDatabaseAccessor db)
        {
            this.db = db;
        }
        public async Task<CourseDetailViewModel> GetCourse(int Id)
        {
            string query = "SELECT Id,Title,Description,ImagePath,Author,Rating,FulPriceAmount,FullPriceCurrency,CurrentPriceAmount,CurrentPriceCurrency FROM Courses Where id=" + Id +
                            "; SELECT Id,Title,Description,Duration FROM Lessons Where CoursesId =" + Id;
            DataSet dataSet = await db.Query(query);
            var courseTable = dataSet.Tables[0];
            if(courseTable.Rows.Count != 1)
            {
                throw new InvalidOperationException($"Did not return exactly 1 row for course {Id}");
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            var lessonDataTable = dataSet.Tables[1];

            foreach(DataRow lessonRow in lessonDataTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
                courseDetailViewModel.Lesson.Add(lessonViewModel);
            }
            return courseDetailViewModel;
        }

        public async  Task<List<CourseViewModel>> GetCourses()
        {
            string query = "SELECT Id,Title,ImagePath,Author,Rating,FulPriceAmount,FullPriceCurrency,CurrentPriceAmount,CurrentPriceCurrency FROM Courses";
            DataSet dataSet = await db.Query(query);
            var dataTable = dataSet.Tables[0];
            var courseList = new List<CourseViewModel>();
            foreach(DataRow courseRow in dataTable.Rows)
            {
                CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
                courseList.Add(course);
            }
            return courseList;
        }
    }
}
