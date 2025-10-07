using System;
using System.Collections.Generic;

using JOIEnergy.Enums;
using JOIEnergy.Repositories;
using JOIEnergy.Services;

using Xunit;

namespace JOIEnergy.Tests
{
    public class AccountServiceTest
    {
        private const string PRICE_PLAN_ID = "price-plan-id";
        private const string SMART_METER_ID = "smart-meter-id";

        private AccountService accountService;

        public AccountServiceTest()
        {
            var accountRepository = new TestAccountRepository();
            accountService = new AccountService(accountRepository);
        }

        [Fact]
        public void GivenTheSmartMeterIdReturnsThePricePlanId()
        {
            var result = accountService.GetPricePlanIdForSmartMeterId(SMART_METER_ID);
            Assert.Equal(PRICE_PLAN_ID, result);
        }
        
        [Fact]
        public void GivenAnUnknownSmartMeterIdReturnsNull()
        {
            var result = accountService.GetPricePlanIdForSmartMeterId("non-existent");
            Assert.Null(result);
        }

        private class TestAccountRepository : IAccountRepository
        {
            private readonly Dictionary<string, string> _smartMeterToPricePlan;

            public TestAccountRepository()
            {
                _smartMeterToPricePlan = new Dictionary<string, string>
                {
                    { SMART_METER_ID, PRICE_PLAN_ID }
                };
            }

            public string GetPricePlanForSmartMeter(string smartMeterId)
            {
                return _smartMeterToPricePlan.TryGetValue(smartMeterId, out var pricePlanId)
                    ? pricePlanId
                    : null;
            }

            public Dictionary<string, string> GetAllMeterToPricePlanMappings()
            {
                return _smartMeterToPricePlan;
            }
        }
    }
}
