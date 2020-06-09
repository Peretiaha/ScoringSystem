using ScoringSystem.BLL.Interfaces;
using ScoringSystem.DAL.UnitOfWork;
using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.BLL.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRolesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(UsersRoles entity)
        {
            var repository = _unitOfWork.GetRepository<UsersRoles>();
            repository.Insert(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public void DeleteConnection(int userId, int roleId)
        {
            var repository = _unitOfWork.GetRepository<UsersRoles>();
            var userRole = repository.GetSingle(x=>x.RoleId == roleId);
            repository.Delete(userRole);
            _unitOfWork.Commit();
        }

        public void Edit(UsersRoles entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsersRoles> GetAll(int userId)
        {
            return _unitOfWork.GetRepository<UsersRoles>().GetMany(x => x.UserId == userId);
        }
    }
}
