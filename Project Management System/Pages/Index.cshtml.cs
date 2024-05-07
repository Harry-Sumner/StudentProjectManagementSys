using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;

namespace Project_Management_System.Pages
{
   // [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SPMS_Context _context;
        public IndexModel(SPMS_Context context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Topic> Topic { get; set; } = default!;
        [BindProperty]
        public string Search { get; set; }

        public void OnGet()
        {
            Topic = _context.Topic.FromSqlRaw("Select * FROM Topic").ToList();
        }


        public IActionResult OnPostSearch()
        {
            Topic = _context.Topic.FromSqlRaw("SELECT * FROM Topic WHERE TopicName LIKE '" + Search + "%'").ToList();
            return Page();
        }
        
    }
}
