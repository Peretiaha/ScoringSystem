using ScoringSystem.BLL.Interfaces;
using ScoringSystem.DAL.UnitOfWork;
using ScoringSystem.Model.Entities;
using System.Collections.Generic;

namespace ScoringSystem.BLL.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(BankAccount entity)
        {
            _unitOfWork.GetRepository<BankAccount>().Insert(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int entityId)
        {
            var entity = _unitOfWork.GetRepository<BankAccount>().GetSingle(x => x.BankAccountId == entityId);
            _unitOfWork.GetRepository<BankAccount>().Delete(entity);
            _unitOfWork.Commit();
        }

        public void Edit(BankAccount entity)
        {
            _unitOfWork.GetRepository<BankAccount>().Update(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<BankAccount> GetAll(int userId)
        {
            return _unitOfWork.GetRepository<BankAccount>().GetMany(x => x.User.UserId == userId, null, x=>x.User);
        }

        public BankAccount GetById(int bankAccountId)
        {
            return _unitOfWork.GetRepository<BankAccount>().GetSingle(x=>x.BankAccountId == bankAccountId);
        }
    }
}
