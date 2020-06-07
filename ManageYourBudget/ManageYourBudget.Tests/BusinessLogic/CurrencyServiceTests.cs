using System;
using System.Linq;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.BussinessLogic.Services;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NUnit.Framework;

namespace ManageYourBudget.Tests.BusinessLogic
{
    [TestFixture]
    public class CurrencyServiceTests: BaseTestClass
    {
        private Mock<IExchangeClient> _exchangeClientMock;
        private Mock<ICacheService> _distributedCacheServiceMock;

        [SetUp]
        public void SetUp()
        {
            _exchangeClientMock = new Mock<IExchangeClient>();
            _distributedCacheServiceMock = new Mock<ICacheService>();
        }

        [Test]
        public async Task FillCacheWithRates_CallExchangeServiceAndCacheService_ExactlyHowManyIsPairsBetweenCurrencies()
        {
            var allValues = Enum.GetValues(typeof(SupportedCurrencies)).Cast<SupportedCurrencies>()
                .Select(x => x.ToString()).ToList();
            var numberOfPairs = allValues.SelectMany(x => allValues, (From, To) => new { From, To }).Count();
            _exchangeClientMock.Setup(x => x.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new CurrencyRateDto());

            var sut = new CurrencyService(_distributedCacheServiceMock.Object, _exchangeClientMock.Object);
            await sut.FillCacheWithRates();

            _exchangeClientMock.Verify(x => x.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(numberOfPairs));
            _distributedCacheServiceMock.Verify(x => x.Set<CurrencyRateDto>(It.IsAny<string>(), It.IsAny<CurrencyRateDto>(), It.IsAny<TimeSpan>()), Times.Exactly(numberOfPairs));
        }
    }
}
