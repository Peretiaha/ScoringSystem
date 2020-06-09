using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.Model.Entities;
using ScoringSystem.Web.ViewModels;

namespace ScoringSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRolesService _userRolesService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public CustomerController(IUserService userService, IRoleService roleService, IUserRolesService userRolesService, IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _userRolesService = userRolesService;
            _mapper = mapper;
        }

        [HttpGet("customers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IEnumerable<SmallCustomerViewModel> GetAll()
        {
            var customersViewModel = _mapper.Map<IEnumerable<SmallCustomerViewModel>>(_userService.GetAllCustomers());

            foreach(var customer in customersViewModel)
            {
                foreach(var role in customer.Roles)
                {
                    role.UsersRoles = null;
                }
            }

            return customersViewModel;
        }

        [HttpPost("change-role")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult ChangeUserRoles([FromBody] ChangeRolesViewModel changeRolesViewModel)
        {
            var user = _userService.GetUserById(changeRolesViewModel.userId);

            try
            {
                var userRoles = _userRolesService.GetAll(changeRolesViewModel.userId).ToList();

                if (userRoles.Count != 0)
                {
                    _userRolesService.DeleteConnection(changeRolesViewModel.userId, userRoles.FirstOrDefault().RoleId);
                }

                foreach (var userRole in changeRolesViewModel.Roles)
                {
                    var role = new UsersRoles
                    {
                        RoleId = _roleService.GetRoleByName(userRole).RoleId,
                        UserId = changeRolesViewModel.userId
                    };

                    _userRolesService.Create(role);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
