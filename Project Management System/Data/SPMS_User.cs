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
        public byte[] ProfilePicture{ get; set; }
    }

    public class SPMS_Student : SPMS_User
    {
        [PersonalData, StringLength(7)]
        public string StudentID { get; set; }
    }

    public class SPMS_Staff : SPMS_User
    {
    }
}
