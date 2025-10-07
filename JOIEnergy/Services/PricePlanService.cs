using System;
using System.Collections.Generic;
using System.Linq;

using JOIEnergy.Domain;
using JOIEnergy.Repositories;

namespace JOIEnergy.Services
{
    public class PricePlanService : IPricePlanService
    {
        public interface Debug { void Log(string s); };

        private readonly IPricePlanRepository _pricePlanRepository;
        private IMeterReadingService _meterReadingService;
        private IAccountService _accountService;

        public PricePlanService(IPricePlanRepository pricePlanRepository, IMeterReadingService meterReadingService, IAccountService accountService)
        {
            _pricePlanRepository = pricePlanRepository;
            _meterReadingService = meterReadingService;
            _accountService = accountService;
        }

        private decimal calculateAverageReading(List<ElectricityReading> electricityReadings)
        {
            var newSummedReadings = electricityReadings.Select(readings => readings.Reading).Aggregate((reading, accumulator) => reading + accumulator);

            return newSummedReadings / electricityReadings.Count();
        }

        private decimal calculateTimeElapsed(List<ElectricityReading> electricityReadings)
        {
            var first = electricityReadings.Min(reading => reading.Time);
            var last = electricityReadings.Max(reading => reading.Time);

            return (decimal)(last - first).TotalHours;
        }
        private decimal calculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
        {
            var average = calculateAverageReading(electricityReadings);
            var timeElapsed = calculateTimeElapsed(electricityReadings);
            var averagedCost = average*timeElapsed;
            return Math.Round(averagedCost * pricePlan.UnitRate, 3);
        }

        public Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId)
        {
            List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);

            if (!electricityReadings.Any())
            {
                return new Dictionary<string, decimal>();
            }

            var pricePlans = _pricePlanRepository.GetAll();
            return pricePlans.ToDictionary(plan => plan.PlanName, plan => calculateCost(electricityReadings, plan));
        }

        public decimal GetConsumptionCostOfLastWeekElectricityReadingsForEachPricePlan(string smartMeterId)
        {
            List<ElectricityReading> electricityReadings = _meterReadingService.GetLastWeekReadings(smartMeterId);

            if (!electricityReadings.Any())
            {
                return default;
            }

            var pricePlanId = _accountService.GetPricePlanIdForSmartMeterId(smartMeterId);
            var pricePlan = _pricePlanRepository.GetById(pricePlanId);

            return calculateCost(electricityReadings, pricePlan);
            
        }

    }
}
