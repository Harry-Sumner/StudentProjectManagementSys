using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly SPMS_Context _db;
        private readonly UserManager<SPMS_Student> _userManager;
        private readonly SignInManager<SPMS_User> _signInManager;
        public IndexModel(Project_Management_System.Data.SPMS_Context context, SPMS_Context db, UserManager<SPMS_Student> userManager, SignInManager<SPMS_User> signInManager)
        {
            _context = context;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public IList<Topic> Topic { get; set; } = new List<Topic>();

        // Bind the search query parameter from the URL
        public string Search { get; set; }

        public void OnGet()
        {
            Topic = _context.Topic.FromSqlRaw("Select * FROM Topic").ToList();
        }

        public async Task<IActionResult> OnPostAddAsync(int topicID)
        {
            var user = await _userManager.GetUserAsync(User); //Get details of logged in user


            var topics = _db.TopicBasket //select data from database and assign to item
                .FromSqlRaw("SELECT * FROM TopicBasket WHERE TopicID = {0}" +
                " AND StudentID = {1}", topicID, user.Id)
                .ToList()
                .FirstOrDefault();

            if (topics == null)
            {
                TopicBasket newTopic = new TopicBasket  //If item isnt already in the basket then add it and save changes
                {
                    
                    StudentID = user.Id,
                    TopicID = topicID,

                };
                _db.TopicBasket.Add(newTopic);
                await _db.SaveChangesAsync();
            }
            else //Topic is already added
            {
               
            }

            return RedirectToPage();
        }



        public IActionResult OnPostSearch()
        {
            Topic = _context.Topic.FromSqlRaw("SELECT * FROM Topic WHERE TopicName LIKE '" + Search + "%'").ToList();
            return Page();
        }
    }
}
