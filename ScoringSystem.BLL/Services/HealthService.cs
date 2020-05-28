using ScoringSystem.BLL.Interfaces;
using ScoringSystem.DAL.UnitOfWork;
using ScoringSystem.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ScoringSystem.BLL.Services
{
    public class HealthService : IHealthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HealthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Health entity)
        {
            _unitOfWork.GetRepository<Health>().Insert(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int entityId)
        {
            var health = _unitOfWork.GetRepository<Health>().GetSingle(x=>x.HealthId == entityId);
            _unitOfWork.GetRepository<Health>().Delete(health);
            _unitOfWork.Commit();
        }

        public void Edit(Health entity)
        {
            _unitOfWork.GetRepository<Health>().Update(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<Health> GetAll(int userId)
        {
            return _unitOfWork.GetRepository<Health>().GetMany(x => x.UsersHealth.FirstOrDefault().UserId == userId);
        }

        public Health GetById(int id)
        {
            return _unitOfWork.GetRepository<Health>().GetSingle(x=>x.HealthId == id);
        }
    }
}
