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
        public IList<Topic> Topic { get; set; } = default!;

        public IList<StaffDivision> StaffDivisions = default!;
        public IList<StaffInterest> StaffInterest { get; private set; }


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

            

            return Page();
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
                    user.ProfilePicture = imageBytes;
                    await _userManager.UpdateAsync(user);
                    await _signInManager.RefreshSignInAsync(user);
                }
            }

            return RedirectToPage();
        }
    }
}
