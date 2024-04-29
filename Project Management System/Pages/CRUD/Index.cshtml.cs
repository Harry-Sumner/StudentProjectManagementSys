﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;

namespace Project_Management_System.Pages.CRUD
{
    public class IndexModel : PageModel
    {
        private readonly Project_Management_System.Data.Project_Management_SystemContext _context;

        public IndexModel(Project_Management_System.Data.Project_Management_SystemContext context)
        {
            _context = context;
        }

        public IList<Topic> Topic { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Topic = await _context.Topics.ToListAsync();
        }
    }
}
