using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScoringSystem.BLL.Hash;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.Model.Entities;
using ScoringSystem.Web.Authorization;
using ScoringSystem.Web.ViewModels;

namespace ScoringSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IHealthService _healthService;
        private readonly IRoleService _roleService;
        private readonly IAddressService _addressService;
        private readonly ITokenFactory _tokenFactory;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IRoleService roleService, IAddressService addressService,
            IHealthService healthService, IBankAccountService bankAccountService, ITokenFactory tokenFactory, IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _addressService = addressService;
            _healthService = healthService;
            _bankAccountService = bankAccountService;
            _tokenFactory = tokenFactory;
            _mapper = mapper;
        }

        [HttpPost("{userId}/uploadImage"), DisableRequestSizeLimit]
        public IActionResult Upload(int userId)
        {
            try
            {
                var file = Request.Form.Files[0];

                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)file.Length);
                }
                var user = _userService.GetUserById(userId);
                user.Photo = imageData;
                _userService.Edit(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if(_userService.GetUserByEmail(registerViewModel.Email) == null)
                {
                    var user = _mapper.Map<RegisterViewModel, User>(registerViewModel);
                    var role = _roleService.GetRoleByName("Customer");

                    user.UsersRoles = new List<UsersRoles>
                    {
                        new UsersRoles
                        {
                            Role = role
                        }
                    };
                    
                    _userService.Create(user);
                    var userId = _userService.GetUserByEmail(user.Email).UserId;

                    return Ok(userId);
                }

                return BadRequest("User with same Email is exist");

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

        [HttpPost("health/add/{userId}")]
        public IActionResult AddHealth(int userId, HealthViewModel healthViewModel)
        {
            if (ModelState.IsValid)
            {
                var health = _mapper.Map<Health>(healthViewModel);
                health.AnalizDate = DateTime.Now;
                health.UsersHealth = new List<UsersHealth>
                {
                    new UsersHealth
                    {
                        UserId = userId
                    }
                };


                _healthService.Create(health);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("health/edit/{healthId}")]
        public IActionResult EditHealth(int addressId, AddressViewModel addressViewModel)
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

        [HttpPost("bankAccount/add/{userId}")]
        public IActionResult AddBankAccount(int userId, BankAccountViewModel bankAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                var bankAccount = _mapper.Map<BankAccount>(bankAccountViewModel);               
                bankAccount.BankId = bankAccount.Bank.BankId;
                bankAccount.UserId = userId;

                bankAccount.Bank = null;
                bankAccount.User = null;
                _bankAccountService.Create(bankAccount);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("address/edit/{bankAccountId}")]
        public IActionResult EditBankAccount(int bankAccountId, BankAccountViewModel bankAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                var bankAccount = _mapper.Map<BankAccount>(bankAccountViewModel);
                bankAccount.BankAccountId = bankAccountId;

                _bankAccountService.Edit(bankAccount);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{userId}")]
        public CustomerViewModel GetUserById(int userId)
        {
            var user = _mapper.Map<CustomerViewModel>(_userService.GetUserById(userId));
            
            if (user == null)
            {
                return null;
            }

            if (user.Address != null)
            {
                user.Address.User = null;
            }

            return user;
        }

        [HttpPost]
        public IActionResult Token([FromBody] LoginViewModel credentials)
        {
            var user = _userService.GetUserByEmail(credentials.Email);
            var hashhing = new Hashhing();
            if (user != null && hashhing.VerifyHashPassword(user.Password, credentials.Password))
            {
                return Ok(GenerateTokenForUser(user));
            }

            return BadRequest("Invalid credentials.");
        }

        private string GenerateTokenForUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(user.UsersRoles.Select(role => new Claim(ClaimTypes.Role, role.Role.Name)));

            return _tokenFactory.Create(claims);
        }

    }
}