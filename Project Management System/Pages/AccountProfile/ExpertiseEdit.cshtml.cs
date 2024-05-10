using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using Project_Management_System.Migrations;

namespace Project_Management_System.Pages.AccountProfile
{
    public class ExpertiseEditModel : PageModel
    {
        private readonly UserManager<SPMS_Staff> _UserManager;
        private readonly SPMS_Context _context;
        private readonly SPMS_Context _db;

        public ExpertiseEditModel(SPMS_Context context, SPMS_Context db, UserManager<SPMS_Staff> userManager)
        {
            _UserManager = userManager;
            _context = context;
            _db = db;
        }

        [BindProperty]
        public StaffExpertise StaffExpertise { get; set; }

        [BindProperty]
        public int ExpertiseID { get; set; }

        public IList<StaffExpertise> StaffExpertises { get; set; } = new List<StaffExpertise>();

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _UserManager.GetUserAsync(User);

            var expertise = await _context.StaffExpertise.FirstOrDefaultAsync();
            if (expertise == null)
            {
                return NotFound();
            }
            StaffExpertise = expertise;

            StaffExpertises = await _context.StaffExpertise.FromSqlRaw("SELECT * FROM StaffExpertise WHERE StaffID = {0}", user.Id).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string command)
        {
            if (command == "Delete")
            {
                var expertiseToDelete = await _context.StaffExpertise.FindAsync(ExpertiseID);

                if (expertiseToDelete == null)
                {
                    return NotFound();
                }

                _context.StaffExpertise.Remove(expertiseToDelete);
                await _context.SaveChangesAsync();

                return RedirectToPage("/AccountProfile/StaffProfile");
            }
            else if (command == "Save")
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/AccountProfile/StaffProfile");

            }
            var expertiseToUpdate = await _context.StaffExpertise.FindAsync(ExpertiseID);

            if (expertiseToUpdate == null)
            {
                return NotFound();
            }

            expertiseToUpdate.Expertise = expertiseToUpdate.Expertise;
        

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpertiseExists(ExpertiseID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/AccountProfile/StaffProfile");
        }




        private bool ExpertiseExists(int id)
        {
            return _context.StaffExpertise.Any(e => e.ExpertiseID == id);
        }
    }
}
