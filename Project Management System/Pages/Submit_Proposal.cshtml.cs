using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;

namespace Project_Management_System.Pages
{
    public class Submit_ProposalModel : PageModel
    {
        public UndergraduateProposal proposal = new(); //Create new instance of Proposal
        private readonly SPMS_Context _db;
        private readonly UserManager<SPMS_Student> _UserManager;
        private readonly SignInManager<SPMS_User> _signInManager;
        private readonly Project_Management_System.Data.SPMS_Context _context;
        public IList<TopicBasket> TopicsBasket { get; private set; } //Create a list implementing CheckoutItem class called Items; declare getters and setters.

        public IList<TopicBasket> TopicBasket { get; set; }
        public IList<Course> Course { get; set; }
        public IList<Topic> Topics { get; private set; }

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

            if (_context.TopicBasket != null && user != null)
            {
                TopicsBasket = await _context.TopicBasket.ToListAsync();
                /*foreach(var basket in TopicsBasket.Where(i => i.StudentID == user.Id))
                {
                    TopicBasket.Add(basket);
                   
                }*/
            }

            if (_context.Topic != null)
            {
                Topics = await _context.Topic.ToListAsync();
            }

            if (_context.Course != null)
            {
                Course = await _context.Course.ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostSubmitAsync()
        {
            var user = await _UserManager.GetUserAsync(User);
            proposal.StudentID = user.Id;
           
         

            await _db.SaveChangesAsync(); //save all changes to sql database
            return RedirectToPage("/Index"); //return to home page once order complete
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id) //takes id passed from button
        {
            var users = await _UserManager.GetUserAsync(User);
            

            return RedirectToPage(); //return to checkout page
        }

        public async Task<IActionResult> OnPostAddAsync(int id) //id of item is passed in via button page handler
        {
            var users = await _UserManager.GetUserAsync(User);
           

            return RedirectToPage(); //return back to checkout upon completion
        }
    }
}
