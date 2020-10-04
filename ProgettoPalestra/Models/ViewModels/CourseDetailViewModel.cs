using ProgettoPalestra.Models.Enums;
using ProgettoPalestra.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProgettoPalestra.Models.ViewModels
{
    public class CourseDetailViewModel: CourseViewModel
    {
        public string Description { get; set; }
        public List<LessonViewModel> Lesson { get; set; }
        public TimeSpan TotalCourseDuration
        {
            get => TimeSpan.FromSeconds(Lesson?.Sum(l => l.Duration.TotalSeconds) ?? 0);
        }

        public static CourseDetailViewModel FromDataRow(DataRow courseRow)
        {
            var courseDetailViewModel = new CourseDetailViewModel
            {
                Title = (string)courseRow["Title"],
                Description = Convert.ToString(courseRow["Description"]),
                ImagePath = (string)courseRow["ImagePath"],
                Author = (string)courseRow["Author"],
                Rating = Convert.ToDouble(courseRow["Rating"]),
                FullPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(courseRow["FullPriceCurrency"])),
                    Convert.ToDecimal(courseRow["FulPriceAmount"])
                    ),
                CurrentPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(courseRow["CurrentPriceCurrency"])),
                    Convert.ToDecimal(courseRow["CurrentPriceAmount"])
                    ),
                Id = Convert.ToInt32(courseRow["Id"]),
                Lesson = new List<LessonViewModel>()


            };
            return courseDetailViewModel;
        }
    }
}
