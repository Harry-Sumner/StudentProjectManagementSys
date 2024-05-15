using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;

namespace Project_Management_System.Pages.AccountProfile
{
    public class IndexModel : PageModel
    {
        private readonly Project_Management_System.Data.SPMS_Context _context;

        public IndexModel(Project_Management_System.Data.SPMS_Context context)
        {
            _context = context;
        }

        public IList<Topic> Topic { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Topic = await _context.Topic.ToListAsync();
        }
    }
}
