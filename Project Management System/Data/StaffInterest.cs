using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Data
{
    public class StaffInterest
    {
        [Required]
        public int InterestID { get; set; }
        [Required]
        public string StaffID { get; set; }
        [Required]
        public string Interest { get; set; }

    }
}
