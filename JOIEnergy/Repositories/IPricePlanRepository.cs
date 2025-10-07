using System.Collections.Generic;
using System.Linq;

using JOIEnergy.Data;
using JOIEnergy.Domain;

namespace JOIEnergy.Repositories
{
    public interface IPricePlanRepository
    {
        List<PricePlan> GetAll();
        PricePlan GetById(string planId);
    }

    public class InMemoryPricePlanRepository : IPricePlanRepository
    {
        private readonly List<PricePlan> _pricePlans;

        public InMemoryPricePlanRepository()
        {
            _pricePlans = SeedData.GetPricePlans();
        }

        public List<PricePlan> GetAll() => _pricePlans;
        public PricePlan GetById(string planId) => _pricePlans.FirstOrDefault(p => p.PlanName == planId);
    }
}
