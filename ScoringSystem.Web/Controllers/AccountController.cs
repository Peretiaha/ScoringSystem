using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("all")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IEnumerable<UserViewModel> GetAll()
        {
            var users = _userService.GetAllUsers().ToList();

            var usersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(users).ToList();

            for (var i =0; i< usersViewModel.Count; i++)
            {
                if (users[i].UsersHealth.ToList().Count > 0)
                {
                    usersViewModel[i].WeightAverage = (int)users[i].UsersHealth.Select(x => x.Health.Weight).ToList().Average();
                    usersViewModel[i].HeartRateAverage = (int)users[i].UsersHealth.Select(x => x.Health.HeartRate).ToList().Average();
                    usersViewModel[i].BilurubinAverage = (int)users[i].UsersHealth.Select(x => x.Health.Bilirubin).ToList().Average();
                    usersViewModel[i].LastanalizDate = users[i].UsersHealth.Select(x => x.Health.AnalizDate).OrderByDescending(x=>x.Date).FirstOrDefault();
                }

                if (users[i].BankAccounts.ToList().Count > 0)
                {
                    usersViewModel[i].TotalCredit = users[i].BankAccounts.Select(x => x.Credit).ToList().Sum();
                    usersViewModel[i].TotalDebt = users[i].BankAccounts.Select(x => x.Debt).ToList().Sum();
                }
            }

            return usersViewModel;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPut("bankAccount/edit/{bankAccountId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        [HttpGet("profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public CustomerViewModel Profile()
        {
            var userId = int.Parse(User.Claims.First(x=>x.Type == "userId").Value);

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

        [HttpGet("login/{email}/{password}")]
        public string Token(string email, string password)
        {
            var user = _userService.GetUserByEmail(email);
            var hashhing = new Hashhing();
            if (user != null && hashhing.VerifyHashPassword(user.Password, password))
            {
                var token = GenerateTokenForUser(user);
                return token;
            }

            return null;
        }


        [HttpPost("login")]
        public IActionResult Token([FromBody] LoginViewModel credentials)
        {
            var user = _userService.GetUserByEmail(credentials.Email);
            var hashhing = new Hashhing();
            if (user != null && hashhing.VerifyHashPassword(user.Password, credentials.Password))
            {
                var token = GenerateTokenForUser(user);
                return Ok(new { token });
            }

            return BadRequest("Invalid credentials.");
        }

        private string GenerateTokenForUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("email", user.Email)
            };

            claims.AddRange(user.UsersRoles.Select(role => new Claim("roles", role.Role.Name)));

            return _tokenFactory.Create(claims);
        }

    }
}