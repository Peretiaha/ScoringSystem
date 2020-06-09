using AutoMapper;
using ScoringSystem.BLL.Hash;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.DAL.UnitOfWork;
using ScoringSystem.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
            var repository = _unitOfWork.GetRepository<User>();
            repository.Update(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<User> GetAllCustomers()
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();

            var adminRole = roleRepository.GetSingle(x => x.Name == "Admin");

            var users = _unitOfWork.GetRepository<User>().GetMany(x=>x.UsersRoles.All(w=>w.RoleId != adminRole.RoleId),null, x=>x.UsersRoles);

            foreach (var user in users)
            {
                foreach (var userRole in user.UsersRoles)
                {
                    userRole.Role = roleRepository.GetSingle(x => x.RoleId == userRole.RoleId);
                }
            }

            return users;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _unitOfWork.GetRepository<User>().GetMany(null, null, x => x.Address, x => x.BankAccounts, x => x.UsersHealth);

            foreach(var user in users)
            {
                if (user.Address != null)
                {
                    user.Address.User = null;
                }

                if (user.BankAccounts != null)
                {
                    foreach(var bank in user.BankAccounts)
                    {
                        bank.User = null;
                    }
                }

                if (user.UsersHealth != null)
                {
                    foreach (var health in user.UsersHealth)
                    {
                        health.User = null;
                        health.Health = _unitOfWork.GetRepository<Health>().GetSingle(x => x.HealthId == health.HealthId);
                    }
                }
            }

            return users;
        }

        public User GetUserByEmail(string email)
        {
            var user = _unitOfWork.GetRepository<User>().GetSingle(x => x.Email == email, x=>x.UsersRoles, x=>x.UsersHealth);
            user?.UsersRoles.ToList().ForEach(x => x.Role = _unitOfWork.GetRepository<Role>().GetSingle(q => q.RoleId == x.RoleId));
            user?.UsersHealth.ToList().ForEach(x => x.Health = _unitOfWork.GetRepository<Health>().GetSingle(q => q.HealthId == x.HealthId));

            return user;
        }

        public User GetUserById(int userId)
        {
            var user = _unitOfWork.GetRepository<User>().GetSingle(x => x.UserId == userId, x => x.Address, x=>x.BankAccounts, x=>x.UsersHealth);

            foreach(var userHealth in user.UsersHealth)
            {
                userHealth.User = null;
                userHealth.Health = _unitOfWork.GetRepository<Health>().GetSingle(x=>x.HealthId == userHealth.HealthId);
                userHealth.Health.UsersHealth = null;
            }

            foreach (var account in user.BankAccounts)
            {
                account.Bank = _unitOfWork.GetRepository<Bank>().GetSingle(x => x.BankId == account.BankId);
                account.Bank.BankAccounts = null;
                account.User = null;
            }

            user.UsersRoles = _unitOfWork.GetRepository<UsersRoles>().GetMany(x=>x.UserId == userId);

            foreach (var userRoles in user.UsersRoles)
            {
                userRoles.User = null;
            }

            return user;
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
