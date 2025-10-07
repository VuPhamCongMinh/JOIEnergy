using System.Collections.Generic;

using JOIEnergy.Data;

namespace JOIEnergy.Repositories
{
    public interface IAccountRepository
    {
        string GetPricePlanForSmartMeter(string smartMeterId);
        Dictionary<string, string> GetAllMeterToPricePlanMappings();
    }

    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly Dictionary<string, string> _smartMeterToPricePlan;

        public InMemoryAccountRepository()
        {
            _smartMeterToPricePlan = SeedData.GetSmartMeterToPricePlanMappings();
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
