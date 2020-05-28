using AutoMapper;
using ScoringSystem.Model.Entities;
using ScoringSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, User>();
            CreateMap<Bank, BankViewModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<User, LoginViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<User, CustomerViewModel>().ReverseMap();
            CreateMap<Health, HealthViewModel>().ReverseMap();
            CreateMap<BankAccount, BankAccountViewModel>().ReverseMap();

        }
    }
}
