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

namespace Project_Management_System.Pages.AccountProfile
{
    [Authorize]

    public class ExpertiseAddModel : PageModel
    {
        private readonly UserManager<SPMS_Staff> _UserManager;
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public ExpertiseAddModel(Project_Management_System.Data.SPMS_Context context, UserManager<SPMS_Staff> userManager)
        {
            _UserManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StaffExpertise StaffExpertise { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Clear();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var currentExpertise = _context.StaffExpertise.FromSqlRaw("SELECT * FROM StaffExpertise")
                .OrderByDescending(b => b.ExpertiseID)
                .FirstOrDefault();
            if(currentExpertise != null)
            {
                StaffExpertise.ExpertiseID = currentExpertise.ExpertiseID + 1; //increment last id by 1
            }
            else
            {
                StaffExpertise.ExpertiseID = 1;
            }

            var user = await _UserManager.GetUserAsync(User);
            StaffExpertise.StaffID = user.Id;

            _context.StaffExpertise.Add(StaffExpertise);
            await _context.SaveChangesAsync();

            return RedirectToPage("/AccountProfile/StaffProfile");
            
        }
    }
}
