using AutoMapper;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.DAL.UnitOfWork;
using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoringSystem.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var roles = _unitOfWork.GetRepository<Role>().GetMany();
            return roles;
        }

        public IEnumerable<Role> GetAllUserRoles(int userId)
        {
            var roles = _unitOfWork.GetRepository<Role>().GetMany(x => x.UsersRoles.Any(q => q.UserId == userId));
            return roles;
        }

        public Role GetRoleByName(string name)
        {
            return _unitOfWork.GetRepository<Role>().GetSingle(x => x.Name == name);
        }

        public void Create(Role entity)
        {
            _unitOfWork.GetRepository<Role>().Insert(entity);
            _unitOfWork.Commit();
        }

        public void Edit(Role entity)
        {
            _unitOfWork.GetRepository<Role>().Update(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var entity = _unitOfWork.GetRepository<Role>().GetSingle(x => x.RoleId == id);
            _unitOfWork.GetRepository<Role>().Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
