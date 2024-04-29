using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;

namespace Project_Management_System.Pages.CRUD
{
    public class DeleteModel : PageModel
    {
        private readonly Project_Management_System.Data.Project_Management_SystemContext _context;

        public DeleteModel(Project_Management_System.Data.Project_Management_SystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Topic Topic { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.FirstOrDefaultAsync(m => m.TopicID == id);

            if (topic == null)
            {
                return NotFound();
            }
            else
            {
                Topic = topic;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                Topic = topic;
                _context.Topics.Remove(Topic);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
