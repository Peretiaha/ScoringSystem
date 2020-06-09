using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.ViewModels
{
    public class ChangeRolesViewModel
    {
        public int userId { get; set; }

        public string[] Roles { get; set; }
    }
}
