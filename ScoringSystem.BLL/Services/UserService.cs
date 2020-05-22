using AutoMapper;
using ScoringSystem.BLL.Hash;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.DAL.UnitOfWork;
using ScoringSystem.Model.Entities;
using System.Collections.Generic;

namespace ScoringSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void ChangePassword(int userId, string password, string newPassword)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var savedPasswordHash = userRepository.GetSingle(u => u.UserId == userId).Password;
            var hash = new Hashhing();

            if (hash.VerifyHashPassword(savedPasswordHash, password))
            {
                var user = userRepository.GetSingle(x => x.UserId == userId);
                user.Password = hash.HashPassword(newPassword);
                userRepository.Update(user);
                _unitOfWork.Commit();
            }
        }

        public void Create(User entity)
        {
            var hash = new Hashhing();
            entity.Password = hash.HashPassword(entity.Password);
            _unitOfWork.GetRepository<User>().Insert(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int entityId)
        {
            var user = _unitOfWork.GetRepository<User>().GetSingle(x => x.UserId == entityId);
            _unitOfWork.GetRepository<User>().Delete(user);
            _unitOfWork.Commit();
        }

        public void Edit(User entity)
        {
            _unitOfWork.GetRepository<User>().Update(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.GetRepository<User>().GetMany();
        }

        public User GetUserByEmail(string email)
        {
            return _unitOfWork.GetRepository<User>().GetSingle(x => x.Email == email);
        }

        public User GetUserById(int userId)
        {
            return _unitOfWork.GetRepository<User>().GetSingle(x => x.UserId == userId);
        }

        public bool Login(User user)
        {
            var savedPasswordHash = _unitOfWork.GetRepository<User>().GetSingle(u => u.Email == user.Email).Password;
            var hash = new Hashhing();
            if (hash.VerifyHashPassword(savedPasswordHash, user.Password))
            {
                user = _unitOfWork.GetRepository<User>().GetSingle(x => x.Email == user.Email, includes: x => x.UsersRoles);
                foreach (var userRole in user.UsersRoles)
                {
                    userRole.Role = _unitOfWork.GetRepository<Role>().GetSingle(x => x.RoleId == userRole.RoleId);
                }

                return true;
            }

            return false;
        }
    }
}
