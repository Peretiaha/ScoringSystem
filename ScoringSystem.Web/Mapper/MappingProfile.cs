using AutoMapper;
using ScoringSystem.Model.Entities;
using ScoringSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            CreateMap<User, CustomerViewModel>()
                .ForMember(x=>x.Photo, w=>w.MapFrom(x=> "data:image/jpeg;base64," + Convert.ToBase64String(x.Photo)))                
                .ReverseMap();
            CreateMap<Health, HealthViewModel>().ReverseMap();
            CreateMap<BankAccount, BankAccountViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Photo, w => w.MapFrom(x => "data:image/jpeg;base64," + Convert.ToBase64String(x.Photo)))                
                .ForMember(x => x.Country, w => w.MapFrom(x => x.Address.Country))
                .ReverseMap();

            CreateMap<User, SmallCustomerViewModel>()
                .ForMember(x=>x.Roles, w=>w.MapFrom(x=>x.UsersRoles.Select(e=>e.Role)))
                .ReverseMap();

        }
    }
}
