using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Project_Management_System.Data;

namespace Project_Management_System.Pages
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
        public Topic Topic { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Clear();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var currentTopic = _context.Topic.FromSqlRaw("SELECT * FROM Topic")
                .OrderByDescending(b => b.TopicID)
                .FirstOrDefault();
            if(currentTopic != null)
            {
                Topic.TopicID = currentTopic.TopicID + 1; //increment last id by 1
            }
            else
            {
                Topic.TopicID = 1;
            }
            
            _context.Topic.Add(Topic);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
            
        }
    }
}
