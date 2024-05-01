using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Pages.AccountProfile
{
    public class StaffProfileModel : PageModel
    {
        private readonly UserManager<SPMS_User> _userManager;
        private readonly SignInManager<SPMS_User> _signInManager;
        private readonly SPMS_Context _db;
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public StaffProfileModel(Project_Management_System.Data.SPMS_Context context,
            UserManager<SPMS_User> userManager,
            SignInManager<SPMS_User> signInManager,
            SPMS_Context db)
        {
            _context = context;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        public IList<Division> Divisions { get; set; } = default!;

        public IList<StaffDivision> StaffDivisions = default!;


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
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

            return Page();
        }

        public async Task<IActionResult> OnUpdateProfilePicAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            foreach (var file in Request.Form.Files)
            {
                MemoryStream stream = new MemoryStream();
                file.CopyTo(stream);
                user.ProfilePicture = stream.ToArray();

                stream.Close();
                stream.Dispose();
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
