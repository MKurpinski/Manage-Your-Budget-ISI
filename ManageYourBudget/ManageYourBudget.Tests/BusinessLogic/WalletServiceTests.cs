using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.BussinessLogic.Services;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Wallet;
using Moq;
using NUnit.Framework;

namespace ManageYourBudget.Tests.BusinessLogic
{
    [TestFixture]
    public class WalletServiceTests: BaseTestClass
    {
        private Mock<IWalletRepository> _walletRepoMock;
        private Mock<IEmailService> _emailServiceMock;

        [SetUp]
        public void SetUp()
        {
            _walletRepoMock = new Mock<IWalletRepository>();
            _emailServiceMock = new Mock<IEmailService>();
        }

        [Test]
        public async Task ArchiveWallet_WalletDoesNotExist_ShouldReturnFailure()
        {
            _walletRepoMock.Setup(x => x.GetWithoutDependencies(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(default(UserWallet)));

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.ArchiveWallet(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeFalse();
        }

        [Test]
        public async Task ArchiveWallet_WalletExistsAndUserIsAdmin_ShouldReturnSuccess()
        {
            _walletRepoMock.Setup(x => x.GetWithoutDependencies(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new UserWallet{ Role = WalletRole.Admin, Wallet = new Wallet()});
            _walletRepoMock.Setup(x => x.Update(It.IsAny<Wallet>())).Returns(1);


            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.ArchiveWallet(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeTrue();
        }

        [Test]
        public async Task ArchiveWallet_WalletExistsAndUserIsNotAdmin_ShouldReturnSuccess()
        {
            _walletRepoMock.Setup(x => x.GetWithoutDependencies(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new UserWallet { Role = WalletRole.Normal, Wallet = new Wallet() });
            _walletRepoMock.Setup(x => x.Update(It.IsAny<UserWallet>())).Returns(1);
            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.ArchiveWallet(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeTrue();
        }

        [Test]
        public async Task StarWallet_WalletDoesNotExist_ShouldReturnFailure()
        {
            _walletRepoMock.Setup(x => x.GetWithoutDependencies(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(default(UserWallet)));

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.StarWallet(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeFalse();
        }


        [Test]
        public async Task StarWallet_WalletExists_ShouldReturnSuccess()
        {
            _walletRepoMock.Setup(x => x.GetWithoutDependencies(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new UserWallet { Role = WalletRole.Normal, Wallet = new Wallet() });
            _walletRepoMock.Setup(x => x.Update(It.IsAny<UserWallet>())).Returns(1);


            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.StarWallet(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeTrue();
        }

        [Test]
        public async Task UpdateWallet_WalletDoesNotExist_ShouldReturnFailure()
        {
            _walletRepoMock.Setup(x => x.GetWithoutDependencies(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(default(UserWallet)));

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.UpdateWallet(Fixture.Create<UpdateWalletDto>(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeFalse();
        }

        [Test]
        public async Task UpdateWallet_WalletExistsAndUserIsNotAdmin_ShouldReturnFailure()
        {
            _walletRepoMock.Setup(x => x.GetWithoutDependencies(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new UserWallet { Role = WalletRole.Normal, Wallet = new Wallet() });

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.UpdateWallet(Fixture.Create<UpdateWalletDto>(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeFalse();
        }

        [Test]
        public async Task UpdateWallet_WalletExistsAndUserIsAdmin_ShouldReturnSuccess()
        {
            _walletRepoMock.Setup(x => x.GetWithoutDependencies(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new UserWallet { Role = WalletRole.Admin, Wallet = new Wallet() });
            _walletRepoMock.Setup(x => x.Update(It.IsAny<Wallet>())).Returns(1);

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.UpdateWallet(Fixture.Create<UpdateWalletDto>(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeTrue();
        }

        [Test]
        public async Task GetWallet_WalletExists_ShouldReturnSuccessWithWallet()
        {
            _walletRepoMock.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<string>(), false)).ReturnsAsync(new UserWallet { Role = WalletRole.Admin, Wallet = new Wallet() });

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.GetWallet(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Test]
        public async Task GetWallet_WalletDoesNotExist_ShouldReturnFailure()
        {
            _walletRepoMock.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<string>(), false)).ReturnsAsync(default(UserWallet));

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.GetWallet(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeFalse();
            result.Value.Should().BeNull();
        }

        [Test]
        public async Task HasAnyWallet_UserHasWallets_ShouldReturnTrue()
        {
            _walletRepoMock.Setup(x => x.HasAny(It.IsAny<string>())).ReturnsAsync(true);

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.HasAnyWallet(Guid.NewGuid().ToString());

            result.Should().BeTrue();
        }

        [Test]
        public async Task HasAnyWallet_UserHasntGotWallets_ShouldReturnFalse()
        {
            _walletRepoMock.Setup(x => x.HasAny(It.IsAny<string>())).ReturnsAsync(false);

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.HasAnyWallet(Guid.NewGuid().ToString());

            result.Should().BeFalse();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public async Task GetWallets_ShouldReturnWalletsOfUser(int numberOfWallets)
        {
            _walletRepoMock.Setup(x => x.GetAll(It.IsAny<string>())).ReturnsAsync(Enumerable.Range(0, numberOfWallets).Select(_ => new UserWallet{Wallet = new Wallet()}).ToList());

            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.GetWallets(Guid.NewGuid().ToString());

            result.Should().HaveCount(numberOfWallets);
        }

        [Test]
        public async Task CreateWallet_WalletSuccessfullySavedInDb_ShouldReturnSuccess()
        {
            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.CreateWallet(Fixture.Create<AddWalletDto>(), Guid.NewGuid().ToString());

            result.Should().NotBeNull();
        }

        [Test]
        public async Task CreateWallet_WalletUnSuccessfullySavedInDb_ShouldReturnSuccess()
        {
            var sut = new WalletService(Mapper, _walletRepoMock.Object, _emailServiceMock.Object);

            var result = await sut.CreateWallet(Fixture.Create<AddWalletDto>(), Guid.NewGuid().ToString());

            result.Succedeed.Should().BeFalse();
        }
    }
}
