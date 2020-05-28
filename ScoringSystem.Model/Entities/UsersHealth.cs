using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Model.Entities
{
    public class UsersHealth
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int HealthId { get; set; }

        public Health Health { get; set; }
    }
}
