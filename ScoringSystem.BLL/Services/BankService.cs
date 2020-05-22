using AutoMapper;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.DAL.UnitOfWork;
using ScoringSystem.Model.Entities;
using System.Collections.Generic;

namespace ScoringSystem.BLL.Services
{
    public class BankService : IBankService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BankService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(Bank entity)
        {
            _unitOfWork.GetRepository<Bank>().Insert(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int entityId)
        {
            var bank = _unitOfWork.GetRepository<Bank>().GetSingle(x => x.BankId == entityId);
            _unitOfWork.GetRepository<Bank>().Delete(bank);
            _unitOfWork.Commit();
        }

        public void Edit(Bank entity)
        {
            _unitOfWork.GetRepository<Bank>().Update(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<Bank> GetAll()
        {
            return _unitOfWork.GetRepository<Bank>().GetMany();
        }

        public Bank GetBankById(int id)
        {
            return _unitOfWork.GetRepository<Bank>().GetSingle(x => x.BankId == id);
        }
    }
}
