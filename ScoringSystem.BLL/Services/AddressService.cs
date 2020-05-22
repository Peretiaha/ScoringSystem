using AutoMapper;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.DAL.UnitOfWork;
using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;

namespace ScoringSystem.BLL.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(Address entity)
        {
            if (entity.User?.UserId != null)
            {
                var userRepo = _unitOfWork.GetRepository<User>();
                var user = userRepo.GetSingle(x => x.UserId == entity.User.UserId);
                user.Address = entity;
                userRepo.Update(user);
                _unitOfWork.Commit();
            }
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public void Edit(Address entity)
        {
            _unitOfWork.GetRepository<Address>().Update(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<Address> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Address> GetAllByCity(string city)
        {
            throw new NotImplementedException();
        }

        public Address GetById(int addressId)
        {
            throw new NotImplementedException();
        }

        public Address GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
