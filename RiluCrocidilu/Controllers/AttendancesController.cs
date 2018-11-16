using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiluCrocidilu.Models;
using RiluCrocidilu.Models.HomeViewModels;

namespace RiluCrocidilu.Controllers
{
    public class AttendancesController : BaseController
    {

        private AttendanceViewModel attendance;

        public AttendancesController(RiluCrocodiluContext context) : base(context)
        {
            _context = context;
            attendance = new AttendanceViewModel(_context);
        }
        public async Task<IActionResult> Index(int? id)
        {
           
            await GetModules();
            

            return View(await attendance.GetAttendees(id));
        }
    }
}