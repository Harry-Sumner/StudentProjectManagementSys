// Inside your Edit.cshtml.cs file

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;

namespace Project_Management_System.Pages.crud
{
    public class EditModel : PageModel
    {
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public EditModel(Project_Management_System.Data.SPMS_Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Topic Topic { get; set; }

        [BindProperty]
        public int TopicID { get; set; }

        public IList<Topic> Topics { get; set; } = new List<Topic>();

        public async Task<IActionResult> OnGetAsync()
        {

            var topic = await _context.Topic.FirstOrDefaultAsync();
            if (topic == null)
            {
                return NotFound();
            }
            Topic = topic;

            Topics = await _context.Topic.ToListAsync();

            return Page();
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

                _context.Topic.Remove(topicToDelete);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else if (command == "Save")
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
                var topicToUpdate = await _context.Topic.FindAsync(TopicID);

                if (topicToUpdate == null)
                {
                    return NotFound();
                }

                topicToUpdate.TopicName = Topic.TopicName;
                topicToUpdate.TopicDescription = Topic.TopicDescription;
                topicToUpdate.SupervisorID = Topic.SupervisorID;
                topicToUpdate.MarkerID = Topic.MarkerID;

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

                return RedirectToPage("./Index");
            }

        
    

        private bool TopicExists(int id)
        {
            return _context.Topic.Any(e => e.TopicID == id);
        }
    }
}
