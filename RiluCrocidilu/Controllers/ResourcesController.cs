using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RiluCrocidilu.Models;

namespace RiluCrocidilu.Controllers
{
    public class ResourcesController : BaseController
    {

        public ResourcesController(RiluCrocodiluContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            await GetModules();
            return View();
        }
    }
}