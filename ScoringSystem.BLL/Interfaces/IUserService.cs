using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.BLL.Interfaces
{
    public interface IUserService : IService<User>
    {
        bool Login(User user);

        IEnumerable<User> GetAllUsers();

        User GetUserById(int userId);

        User GetUserByEmail(string email);
    }
}
