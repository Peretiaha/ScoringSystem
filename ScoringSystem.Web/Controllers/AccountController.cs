using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.Model.Entities;
using ScoringSystem.Web.ViewModels;

namespace ScoringSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IRoleService roleService, IAddressService addressService, IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpPost("registration")]
        public IActionResult Registration(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<RegisterViewModel, User>(registerViewModel);
                var role = _roleService.GetRoleByName("Customer");

                user.UsersRoles = new List<UsersRoles>()
                {
                    new UsersRoles()
                    {
                        Role = role
                    }
                };
                _userService.Create(user);

                return Ok();
            }

            return BadRequest(registerViewModel);
        }

        [HttpPost("{userId}/changePassword")]
        public IActionResult ChangePassword(int userId, ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                _userService.ChangePassword(userId, changePasswordViewModel.Password, changePasswordViewModel.NewPassword);
                return Ok();
            }

            return BadRequest();
        } 

        [HttpPost("address/add/{userId}")]
        public IActionResult AddAddress(int userId, AddressViewModel addressViewModel)
        {
            if(ModelState.IsValid)
            {
                var address = _mapper.Map<Address>(addressViewModel);
                address.User = new User
                {
                    UserId = userId
                };

                _addressService.Create(address);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("address/edit/{addressId}")]
        public IActionResult EditAddress(int addressId, AddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                var address = _mapper.Map<Address>(addressViewModel);
                address.AddressId = addressId;

                _addressService.Edit(address);

                return Ok();
            }

            return BadRequest();
        }
    }
}