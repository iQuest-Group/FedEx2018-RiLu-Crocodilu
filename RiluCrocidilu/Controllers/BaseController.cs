using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RiluCrocidilu.Models;
using RiluCrocidilu.Models.HomeViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RiluCrocidilu.Controllers
{
    public abstract class BaseController : Controller
    {       
        protected RiluCrocodiluContext _context;
        protected MenuViewModel viewModel;

        public BaseController(RiluCrocodiluContext context)
        {
            _context = context;
            viewModel = new MenuViewModel(_context);
        }

        protected async Task GetModules()
        {
            await viewModel.GetModules();
            ViewData["ModulesModel"] = viewModel;
        }
    }
}
