using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;

namespace Project_Management_System.Pages.AccountProfile
{
    public class AllocateStaffModel : PageModel
    {
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public AllocateStaffModel(Project_Management_System.Data.SPMS_Context context)
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

            var topic =  await _context.Topic.FirstOrDefaultAsync(m => m.TopicID == id);
            if (topic == null)
            {
                return NotFound();
            }
            Topic = topic;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var topicToUpdate = await _context.Topic.FindAsync(Topic.TopicID);

            if (topicToUpdate == null)
            {
                return Page();
            }

            if (Topic.SupervisorID != null)
            {
                topicToUpdate.SupervisorID = Topic.SupervisorID;
            }
            if (Topic.MarkerID != null)
            {
                topicToUpdate.MarkerID = Topic.MarkerID;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(Topic.TopicID))
                {
                    return Page();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/AccountProfile/CoordinatorProfile");
        }

        private bool TopicExists(int id)
        {
            return _context.Topic.Any(e => e.TopicID == id);
        }
    }
}
