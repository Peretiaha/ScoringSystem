using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Model.Entities
{
    public class UsersRoles
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
