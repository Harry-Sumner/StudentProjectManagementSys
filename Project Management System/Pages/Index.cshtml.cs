using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_Management_System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_System.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SPMS_Context _context;
        private readonly SPMS_Context _db;
        private readonly UserManager<SPMS_Student> _userManager;
        private readonly SignInManager<SPMS_User> _signInManager;

        public IndexModel(ILogger<IndexModel> logger, SPMS_Context context, SPMS_Context db, UserManager<SPMS_Student> userManager, SignInManager<SPMS_User> signInManager)
        {
            _logger = logger;
            _context = context;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IList<Topic> Topic { get; set; } = new List<Topic>();
        public IList<Course> Course { get; set; } = new List<Course>();
        // Bind the search query parameter from the URL
        public string Search { get; set; }

        public async Task OnGetAsync(string search)
        {
            // If a search query is provided, filter topics
            if (!string.IsNullOrEmpty(search))
            {
                Topic = await _context.Topic
                    .Where(t => EF.Functions.Like(t.TopicName, $"%{search}%"))
                    .ToListAsync();
            }
            else
            {
                // If no search query is provided, retrieve all topics
                Topic = await _context.Topic.ToListAsync();
            }

            if(_context.Course != null)
            {
                Course = await _context.Course.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAddAsync(int topicID)
        {
            var user = await _userManager.GetUserAsync(User);

            var topics = await _db.TopicBasket
                .FromSqlRaw("SELECT * FROM TopicBasket WHERE TopicID = {0} AND StudentID = {1}", topicID, user.Id)
                .ToListAsync();

            if (topics.Count == 0)
            {
                TopicBasket newTopic = new TopicBasket
                {
                    StudentID = user.Id,
                    TopicID = topicID
                };
                _db.TopicBasket.Add(newTopic);
                await _db.SaveChangesAsync();

                TempData["ShowPopup"] = true;
            }
            else
            {
                // Handle case where topic is already added
                TempData["ShowPopup"] = "Topic is already added to your proposal.";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            await OnGetAsync(Search);
            return Page();
        }
    }
}
