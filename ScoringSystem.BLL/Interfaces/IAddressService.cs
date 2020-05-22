using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.BLL.Interfaces
{
    public interface IAddressService : IService<Address>
    {
        Address GetById(int addressId);

        Address GetByUserId(int userId);

        IEnumerable<Address> GetAll();

        IEnumerable<Address> GetAllByCity(string city);
    }
}
