using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_Management_System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Project_Management_System.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SPMS_Context _context;

        public IndexModel(SPMS_Context context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Topic> Topic { get; set; } = new List<Topic>();

        // Bind the search query parameter from the URL
        public string Search { get; set; }

        // Handle GET request for search functionality
        public void OnGet(string search)
        {
            // If a search query is provided, filter topics
            if (!string.IsNullOrEmpty(search))
            {
                Topic = _context.Topic
                    .Where(t => EF.Functions.Like(t.TopicName, $"%{search}%"))
                    .ToList();
            }
            else
            {
                // If no search query is provided, retrieve all topics
                Topic = _context.Topic.ToList();
            }
        }
    }
}
