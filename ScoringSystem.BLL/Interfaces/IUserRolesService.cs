using ScoringSystem.BLL.Services;
using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.BLL.Interfaces
{
    public interface IUserRolesService : IService<UsersRoles>
    {
        void DeleteConnection(int userId, int roleId);

        IEnumerable<UsersRoles> GetAll(int userId);
    }
}
