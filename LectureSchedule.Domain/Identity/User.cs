using LectureSchedule.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LectureSchedule.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Graduation Graduation { get; set; }

        public string Description { get; set; }

        public UserFunction UserFunction { get; set; }

        public string ProfileImage { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
