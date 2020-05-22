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
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;
        private readonly IMapper _mapper;

        public BankController(IBankService bankService, IMapper mapper)
        {
            _bankService = bankService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<BankViewModel> GetAll()
        {
            var banks = _bankService.GetAll();
            return _mapper.Map<IEnumerable<BankViewModel>>(banks);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var bank = _bankService.GetBankById(id);

            if (bank != null)
            {
                return new ObjectResult(bank);
            }

            return NotFound(id);
        }

        [HttpPost("add")]
        public IActionResult Post([FromBody] BankViewModel bankViewModel)
        {
            if( ModelState.IsValid)
            {
                var bank = _mapper.Map<BankViewModel, Bank>(bankViewModel);
                _bankService.Create(bank);
                return Ok();
            }

            return BadRequest(bankViewModel);
        }

        [HttpPut("edit/{id}")]
        public IActionResult Put(int id, [FromBody] BankViewModel bankViewModel)
        {
            if (ModelState.IsValid)
            {
                var bank = _mapper.Map<Bank>(bankViewModel);
                bank.BankId = id;
                _bankService.Edit(bank);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bankService.Delete(id);
        }
    }
}
