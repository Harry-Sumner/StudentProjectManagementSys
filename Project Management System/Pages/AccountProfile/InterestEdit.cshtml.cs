using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using Project_Management_System.Migrations;

namespace Project_Management_System.Pages.AccountProfile
{
    [Authorize(Roles = "Staff")]
    public class InterestEditModel : PageModel
    {
        private readonly UserManager<SPMS_Staff> _UserManager;
        private readonly SPMS_Context _context;
        private readonly SPMS_Context _db;

        public InterestEditModel(SPMS_Context context, SPMS_Context db, UserManager<SPMS_Staff> userManager)
        {
            _UserManager = userManager;
            _context = context;
            _db = db;
        }

        [BindProperty]
        public StaffInterest StaffInterest { get; set; } = default!;

        [BindProperty]
        public int InterestID { get; set; }

        public IList<StaffInterest> StaffInterests { get; set; } = new List<StaffInterest>();

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _UserManager.GetUserAsync(User);

            var interest = await _context.StaffInterest.FirstOrDefaultAsync();
            if (interest == null)
            {
                return Page();
            }
            StaffInterest = interest;

            StaffInterests = await _context.StaffInterest.FromSqlRaw("SELECT * FROM StaffInterest WHERE StaffID = {0}", user.Id).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string command)
        {
            if (command == "Delete")
            {
                var interestToDelete = await _context.StaffInterest.FindAsync(InterestID);

                if (interestToDelete == null)
                {
                    return Page();
                }

                _context.StaffInterest.Remove(interestToDelete);
                await _context.SaveChangesAsync();

                return RedirectToPage("/AccountProfile/StaffProfile");
            }
            else if (command == "Save")
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/AccountProfile/StaffProfile");

            }
            var interestToUpdate = await _context.StaffInterest.FindAsync(InterestID);

            if (interestToUpdate == null)
            {
                return NotFound();
            }

            interestToUpdate.Interest = StaffInterest.Interest;
        

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterestExists(InterestID))
                {
                    return Page();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/AccountProfile/StaffProfile");
        }




        private bool InterestExists(int id)
        {
            return _context.StaffInterest.Any(e => e.InterestID == id);
        }
    }
}
