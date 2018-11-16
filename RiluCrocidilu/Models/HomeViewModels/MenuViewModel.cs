using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiluCrocidilu.Models.HomeViewModels
{
    public class MenuViewModel
    {
        private readonly RiluCrocodiluContext _context;

        public List<Module> Modules;

        public MenuViewModel(RiluCrocodiluContext context)
        {
            _context = context;
        }
        
        public async Task GetModules()
        {
            Modules = await (from m in _context.Module
                                select m).ToListAsync();

            Schedule sch = new Schedule();

            foreach (var mod in Modules)
            {
                sch = await (from s in _context.Schedule
                               where s.ModuleId == mod.ModuleId
                               select s).FirstOrDefaultAsync();

                if (sch != null)
                    mod.Schedule = sch;

                mod.Lesson = await (from l in _context.Lesson
                                      where l.ModuleId == mod.ModuleId
                                      select l).ToListAsync();
                sch = null;
            }
        }
    }
}
