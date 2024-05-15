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
    [Authorize(Roles = "Student")]
    public class StudentProfileModel : PageModel
    {
        private readonly UserManager<SPMS_Student> _userManager;
        private readonly SignInManager<SPMS_User> _signInManager;
        private readonly SPMS_Context _db;
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public StudentProfileModel(Project_Management_System.Data.SPMS_Context context,
            UserManager<SPMS_Student> userManager,
            SignInManager<SPMS_User> signInManager,
            SPMS_Context db)
        {
            _context = context;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IList<Course> Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (_context.Course != null)
            {
                Course = await _context.Course.ToListAsync();
            }

            return Page();
        }

        [BindProperty]
        public IFormFile UploadedImage { get; set; } = default!;

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
                    user.ProfilePicture = imageBytes;
                    await _userManager.UpdateAsync(user);
                    await _signInManager.RefreshSignInAsync(user);
                }
            }

            return RedirectToPage();
        }
    }
}
