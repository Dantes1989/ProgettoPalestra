using ProgettoPalestra.Models.Enums;
using ProgettoPalestra.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProgettoPalestra.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public double Rating { get; set; }
        public Money FullPrice { get; set; }
        public Money CurrentPrice { get; set; }
       // public List<LessonViewModel> Lessons { get; set; }

        public static CourseViewModel FromDataRow(DataRow courseRow)
        {
            var courseViewModel = new CourseViewModel
            {
                Title = (string)courseRow["Title"],
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
                //Lessons = new List<LessonViewModel>()
                
                
            };
            return courseViewModel;
        }
    }
}
