using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using Project_Management_System.Migrations;

namespace Project_Management_System.Pages
{
    [Authorize(Roles = "Staff, Co-ordinator")]
    public class EditModel : PageModel
    {
        private readonly SPMS_Context _context;
        private readonly SPMS_Context _db;

        public EditModel(SPMS_Context context, SPMS_Context db)
        {
            _context = context;
            _db = db;
        }

        [BindProperty]
        public Topic Topic { get; set; }

        [BindProperty]
        public int TopicID { get; set; }

        public IList<TopicBasket> TopicBasket { get; set; }
        public IList<PostgraduateProposal> PostgraduateProposal { get; set; }
        public IList<UndergraduateProposal> UndergraduateProposal { get; set; }


        public IList<Topic> Topics { get; set; } = new List<Topic>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var topic = await _context.Topic.FirstOrDefaultAsync();
            if (topic == null)
            {
                return NotFound();
            }
            Topic = topic;

            Topics = await _context.Topic.ToListAsync();

            return Page();

            /*if (id == null)
            {
                var topic = await _context.Topic.FirstOrDefaultAsync(m => m.TopicID == id);
                if (topic == null)
                {
                    return NotFound();
                }
                Topic = topic;

                Topics = await _context.Topic.ToListAsync();
            }
            else
            {
                var topic = await _context.Topic.FirstOrDefaultAsync();
                if (topic == null)
                {
                    return NotFound();
                }
                Topic = topic;

                Topics = await _context.Topic.ToListAsync();
            }

            return Page();*/
        }

        public async Task<IActionResult> OnPostAsync(string command)
        {
            if (command == "Delete")
            {
                var topicToDelete = await _context.Topic.FindAsync(TopicID);

                if (topicToDelete == null)
                {
                    return NotFound();
                }

                TopicBasket = _db.TopicBasket //select data from database
                .FromSqlRaw("SELECT * FROM TopicBasket WHERE TopicID = {0}", TopicID)
                .ToList();

                PostgraduateProposal = _db.PostgraduateProposal //select data from database
                .FromSqlRaw("SELECT * FROM PostgraduateProposal WHERE TopicID = {0}", TopicID)
                .ToList();


                UndergraduateProposal = _db.UndergraduateProposal //select data from database
                .FromSqlRaw("SELECT * FROM UndergraduateProposal WHERE TopicID1 = {0} OR TopicID2 = {0} OR TopicID3 = {0}", TopicID)
                .ToList();


                if (UndergraduateProposal != null)
                {
                    foreach (var proposal in UndergraduateProposal)
                    {
                        _db.UndergraduateProposal.Remove(proposal);
                    }
                    await _db.SaveChangesAsync();

                }

                if (PostgraduateProposal != null)
                {
                    foreach (var proposal in PostgraduateProposal)
                    {
                        _db.PostgraduateProposal.Remove(proposal);
                    }
                    await _db.SaveChangesAsync();

                }

                if (TopicBasket != null)
                {
                    foreach (var topic in TopicBasket)
                    {
                        _db.TopicBasket.Remove(topic);
                    }
                    await _db.SaveChangesAsync();

                }
                _context.Topic.Remove(topicToDelete);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Index");
            }
            else if (command == "Save")
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");

            }
            var topicToUpdate = await _context.Topic.FindAsync(TopicID);

            if (topicToUpdate == null)
            {
                return NotFound();
            }

            topicToUpdate.TopicName = Topic.TopicName;
            topicToUpdate.TopicDescription = Topic.TopicDescription;
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
                if (!TopicExists(TopicID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Index");
        }




        private bool TopicExists(int id)
        {
            return _context.Topic.Any(e => e.TopicID == id);
        }
    }
}
