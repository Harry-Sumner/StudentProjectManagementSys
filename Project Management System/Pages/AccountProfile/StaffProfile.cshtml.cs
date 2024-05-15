using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Pages.AccountProfile
{
    [Authorize(Roles = "Staff")]
    public class StaffProfileModel : PageModel
    {
        private readonly UserManager<SPMS_User> _userManager;
        private readonly SignInManager<SPMS_User> _signInManager;
        private readonly SPMS_Context _db;
        private readonly SPMS_Context _context;

        public StaffProfileModel(SPMS_Context context,
            UserManager<SPMS_User> userManager,
            SignInManager<SPMS_User> signInManager,
            SPMS_Context db)
        {
            _context = context;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }
        public IList<Division> Divisions { get; set; } = default!;
        public IList<Topic> Topic { get; set; } = default!;

        public IList<StaffDivision> StaffDivisions = default!;
        public IList<StaffInterest> StaffInterest { get; private set; }
        public IList<StaffExpertise> StaffExpertise { get; private set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(SPMS_User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            if (_context.StaffDivision != null)
            {
                StaffDivisions = await _context.StaffDivision.ToListAsync();
            }

            if (_context.Division != null)
            {
                Divisions = await _context.Division.ToListAsync();
            }

            /*if (_context.StaffInterest!= null)
            {
                StaffInterest = await _context.StaffInterest.ToListAsync();
            }*/

            Topic = _db.Topic //select data from database
                .FromSqlRaw("SELECT * FROM Topic WHERE SupervisorID = {0}", user.Id)
                .ToList();

            StaffInterest = _db.StaffInterest //select data from database
                .FromSqlRaw("SELECT * FROM StaffInterest WHERE StaffID = {0}", user.Id)
                .ToList();

            StaffExpertise = _db.StaffExpertise //select data from database
                .FromSqlRaw("SELECT * FROM StaffExpertise WHERE StaffID = {0}", user.Id)
                .ToList();



            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int DivisionID) //takes id passed from button
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var division = await _db.StaffDivision.FindAsync(user.Id, DivisionID);
                if (division != null)
                {
                    _db.StaffDivision.Remove(division);
                    await _db.SaveChangesAsync();
                }
            }
            //save changes
            return RedirectToPage(); //return to page
        }

        public async Task<IActionResult> OnPostAsync(IFormFile uploadedImage)
        {
            if (uploadedImage != null && uploadedImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await uploadedImage.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    // You can now use the byte array to store the image in your database

                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        user.ProfilePicture = imageBytes;
                        await _userManager.UpdateAsync(user);
                        await _signInManager.RefreshSignInAsync(user);
                    }
                }
            }

            return RedirectToPage();
        }
    }
}
