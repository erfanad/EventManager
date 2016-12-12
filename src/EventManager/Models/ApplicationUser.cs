using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EventManager.Data;

namespace EventManager.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public List<Attendance> Events { get; set; }
        public List<Following> Followers { get; set; }
        public List<Following> Artists { get; set; }
    }
}
