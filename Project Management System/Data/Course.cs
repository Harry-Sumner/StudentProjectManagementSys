using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Data
{
    public class Course
    {
        [Required]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        [Required]
        public int DivisionID { get; set; }

    }
}
