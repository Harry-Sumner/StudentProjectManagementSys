using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using Project_Management_System.Migrations;

namespace Project_Management_System.Pages
{
    public class Submit_ProposalModel : PageModel
    {
        public UndergraduateProposal proposal = new(); //Create new instance of Proposal
        private readonly SPMS_Context _db;
        private readonly UserManager<SPMS_Student> _UserManager;
        private readonly SignInManager<SPMS_User> _signInManager;
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public IList<TopicBasket> TopicBasket { get; set; }
        public IList<Course> Course { get; set; }
        public IList<Topic> Topics { get; private set; }
        public int Priority { get; set; }

        public bool Postgraduate { get; set; }

        public Submit_ProposalModel(Project_Management_System.Data.SPMS_Context context, SPMS_Context db, UserManager<SPMS_Student> userManager, SignInManager<SPMS_User> signInManager)
        {
            _context = context;
            _db = db;
            _UserManager = userManager;
            _signInManager = signInManager;

        }
        public async Task OnGetAsync()
        {
            var user = await _UserManager.GetUserAsync(User);

            if (user != null)
            {
                TopicBasket = _db.TopicBasket //select data from database
                .FromSqlRaw("SELECT * FROM TopicBasket WHERE StudentID = {0}", user.Id)
                .ToList();

                Course = _db.Course //select data from database
                .FromSqlRaw("SELECT * FROM Course WHERE CourseID = {0}", user.CourseID)
                .ToList();

                foreach(var course in Course)
                {
                    if (course.Postgraduate)
                    {
                        Postgraduate = true;
                    }
                }
            }

            if (_context.Topic != null)
            {
                Topics = await _context.Topic.ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostSubmitAsync()
        {
            var user = await _UserManager.GetUserAsync(User);
            proposal.StudentID = user.Id;
           
         

            await _db.SaveChangesAsync(); //save all changes to sql database
            return RedirectToPage("/Index"); //return to home page
        }

        public async Task<IActionResult> OnPostDeleteAsync(int topicID) //takes id passed from button
        {
            var user = await _UserManager.GetUserAsync(User);
            var topic = await _db.TopicBasket.FindAsync(user.Id, topicID);
            _db.TopicBasket.Remove(topic);
            await _db.SaveChangesAsync(); //save changes
            return RedirectToPage(); //return to page
        }
    }
}
