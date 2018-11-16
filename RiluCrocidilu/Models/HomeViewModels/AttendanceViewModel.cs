using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiluCrocidilu.Models.HomeViewModels
{
    public class AttendanceViewModel
    {
        private RiluCrocodiluContext _context;

        
        public AttendanceViewModel(RiluCrocodiluContext context)
        {
            _context = context;
        }

        public async Task<List<DisplayUser>> GetAttendees(int? id)
        {
            List<DisplayUser> userList = new List<DisplayUser>();
            var attendees = await(from u in _context.Attendance
                                where u.LessonId == id
                                select u).ToListAsync();

            foreach (var attendee in attendees)
            {

                userList.Add(new DisplayUser
                {
                    UserId = _context.ModuleUser.Where(u => u.UserId == attendee.UserId).Select(u => u.UserId).First(),
                    UserName = _context.ModuleUser.Where(u => u.UserId == attendee.UserId).Select(u => u.FirstName + " " + u.LastName).First(),
                    IsAttending = attendee.IsAttending
                });
            }

            return userList;
        }

        public class DisplayUser
        {
            public int UserId { get; set; } 
            public string UserName { get; set; }
            public bool? IsAttending { get; set; }
        }
    }
}
