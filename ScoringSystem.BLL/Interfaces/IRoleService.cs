using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.BLL.Interfaces
{
    public interface IRoleService : IService<Role>
    {
        IEnumerable<Role> GetAllRoles();

        IEnumerable<Role> GetAllUserRoles(int userId);

        Role GetRoleByName(string name);
    }
}
