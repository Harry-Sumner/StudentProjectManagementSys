using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Data

{
    public class Division
    {
        [Required]
        public int DivisionID { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public int SchoolID { get; set; }
            
    }
}
