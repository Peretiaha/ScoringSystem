using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.Web.ViewModels;

namespace ScoringSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CustomerController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CustomerViewModel> GetAll()
        {
            var customersViewModel = _mapper.Map<IEnumerable<CustomerViewModel>>(_userService.GetAllUsers());
            return customersViewModel;
        }
    }
}
