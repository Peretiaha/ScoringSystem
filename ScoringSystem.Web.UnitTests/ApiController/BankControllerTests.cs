using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.Model.Entities;
using ScoringSystem.Web.Controllers;
using ScoringSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Web.UnitTests.ApiController
{
    [TestFixture]
    public class BankControllerTests
    {
        private Mock<IBankService> _bankService;
        private Mock<IMapper> _mapper;
        private BankController _bankController;

        public BankControllerTests()
        {
            _bankService = new Mock<IBankService>();
            _mapper = new Mock<IMapper>();
            _bankController = new BankController(_bankService.Object, _mapper.Object);
        }

        [Test]
        public void GetAll_Get_BanksViewModel()
        {
            var banks = new List<Bank>
            {
                new Bank()
            };

            _bankService.Setup(x => x.GetAll()).Returns(banks);
            _mapper.Setup(x=>x.Map<BankViewModel>(banks));

            var result = _bankController.GetAll();

            Assert.IsInstanceOf<IEnumerable<BankViewModel>>(result);
        }

        [Test]
        public void Get_WhenBannkIdIsNotNull_Bank()
        {
            var bank = new Bank { 
                BankId = 1 
            };

            _bankService.Setup(x => x.GetBankById(It.IsAny<int>())).Returns(bank);

            var result = _bankController.Get(bank.BankId);

            Assert.IsInstanceOf<Bank>(bank);
        }

        [Test]
        public void Post_WhenBankViewModelIsNotNull_AddBank()
        {
            var bankViewModel = new BankViewModel();

            _mapper.Setup(x=>x.Map<Bank>(bankViewModel));

            var result = _bankController.Post(bankViewModel);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Post_WhenBankViewModelIsNotNull_EditBank()
        {
            var bankViewModel = new BankViewModel();
            var bank = new Bank
            {
                BankId = 1,
                Name = "test"
            };

            _mapper.Setup(x => x.Map<Bank>(bankViewModel)).Returns(bank);

            var result = _bankController.Put(1 ,bankViewModel);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Delete_WhenBankIdIsNotNull_DeleteBank()
        {
             var result = _bankController.Delete(1);

            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
