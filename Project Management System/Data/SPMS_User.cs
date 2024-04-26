using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Project_Management_System.Data
{
    public class SPMS_User : IdentityUser
    {
        [PersonalData, Required, StringLength(30)]
        public string FirstName { get; set; }

        [PersonalData, Required, StringLength(60)]
        public string Surname { get; set; }

        public string Name { get { return $"{FirstName} {Surname}"; } }

        [PersonalData, StringLength(7)]
        public string StudentID { get; set; }

        [PersonalData, StringLength(7)]
        public string StaffID { get; set; }
    }
}
