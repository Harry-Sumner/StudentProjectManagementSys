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
    [Authorize(Roles = "Staff, Co-ordinator")]

    public class CreateModel : PageModel
    {
        private readonly SPMS_Context _db;
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public CreateModel(Project_Management_System.Data.SPMS_Context context, SPMS_Context db)
        {
            _context = context;
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Course != null)
            {
                Course = await _context.Course.ToListAsync();
            }
            return Page();
        }

        [BindProperty]
        public Topic Topic { get; set; } = default!;

        [BindProperty]
        public CourseTopic CourseTopic { get; set; }

        public IList<Course> Course { get; set; } = default!;

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
            CourseTopic.TopicID = Topic.TopicID;
            _db.CourseTopic.Add(CourseTopic);
            _db.SaveChanges();
            // adds course to topic

            return RedirectToPage("/Index");
        }
    }
}
