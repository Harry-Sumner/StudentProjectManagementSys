using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Project_Management_System.Data;
using Project_Management_System.Migrations;

namespace Project_Management_System.Pages.AccountProfile
{
    [Authorize]

    public class DivisionAddModel : PageModel
    {
        private readonly UserManager<SPMS_Staff> _UserManager;
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public DivisionAddModel(Project_Management_System.Data.SPMS_Context context, UserManager<SPMS_Staff> userManager)
        {
            _UserManager = userManager;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_context.Division != null)
            {
                Division = await _context.Division.ToListAsync();
            }
         
        }

        public IList<Division> Division { get; set; } = default!;

        public IList<StaffDivision> StaffDivisions { get; set; } = default!;

        [BindProperty]
        public StaffDivision StaffDivision { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Clear();
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            var user = await _UserManager.GetUserAsync(User);

            StaffDivisions = await _context.StaffDivision.FromSqlRaw("SELECT * FROM StaffDivision WHERE StaffID = {0}", user.Id).ToListAsync();

            if(StaffDivisions != null)
            {
                foreach(var Division in StaffDivisions)
                {
                    if(Division.DivisionID == StaffDivision.DivisionID)
                    {
                        await OnGetAsync();
                        return Page();
                        
                    }
                }

            }

            StaffDivision.StaffID = user.Id;

            _context.StaffDivision.Add(StaffDivision);
            await _context.SaveChangesAsync();

            return RedirectToPage("/AccountProfile/StaffProfile");
        }
    }
}
