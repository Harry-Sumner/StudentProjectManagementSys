using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Management_System.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Project_Management_System.Pages.crud
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public CreateModel(Project_Management_System.Data.SPMS_Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Topic Topic { get; set; } = new Topic(); // Ensure Topic is initialized

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set SupervisorID here based on the logged-in user, if needed
            // Example: Topic.SupervisorID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _context.Topic.Add(Topic);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}