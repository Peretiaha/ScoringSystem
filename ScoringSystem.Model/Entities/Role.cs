using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Model.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; }

        public IEnumerable<UsersRoles> UsersRoles { get; set; }
    }
}
