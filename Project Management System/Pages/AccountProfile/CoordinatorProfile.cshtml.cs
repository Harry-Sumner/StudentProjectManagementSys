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
    [Authorize(Roles = "Co-ordinator")]
    public class CoordinatorProfileModel : PageModel
    {
        private readonly UserManager<SPMS_User> _userManager;
        private readonly SignInManager<SPMS_User> _signInManager;
        private readonly SPMS_Context _db;
        private readonly SPMS_Context _context;


        public CoordinatorProfileModel(SPMS_Context context,
            UserManager<SPMS_User> userManager,
            SignInManager<SPMS_User> signInManager,
            SPMS_Context db)
        {
            _context = context;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IList<Division> Divisions { get; set; } = default!;

        public IList<StaffDivision> StaffDivisions = default!;

        public IList<Topic> StudentProposals {  get; set; }
     
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (_context.Topic != null)
            {
                StudentProposals = await _context.Topic.Where(i => !string.IsNullOrEmpty(i.StudentID) && string.IsNullOrEmpty(i.MarkerID)).ToListAsync();
            }

            if (_context.StaffDivision != null)
            {
                StaffDivisions = await _context.StaffDivision.ToListAsync();
            }

            if (_context.Division != null)
            {
                Divisions = await _context.Division.ToListAsync();
            }

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
