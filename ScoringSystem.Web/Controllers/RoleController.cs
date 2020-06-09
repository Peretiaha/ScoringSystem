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
using ScoringSystem.Web.Authorization;

namespace ScoringSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ITokenFactory _tokenFactory;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public RoleController(IUserService userService, ITokenFactory tokenFactory, IRoleService roleService, IMapper mapper)
        {
            _userService = userService;
            _tokenFactory = tokenFactory;
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet("roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IEnumerable<Role> GetAll()
        {
            return _roleService.GetAllRoles();
        }
    }
}
