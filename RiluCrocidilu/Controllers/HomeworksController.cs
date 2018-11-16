using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiluCrocidilu.Models;

namespace RiluCrocidilu.Controllers
{
    public class HomeworksController : BaseController
    {
        public HomeworksController(RiluCrocodiluContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            await GetModules();
            var homework = await (from h in _context.Homework
                                   where h.LessonId == id
                                   select h).FirstOrDefaultAsync();

            return View((homework != null) ? homework : new Homework());
        }
    }
}