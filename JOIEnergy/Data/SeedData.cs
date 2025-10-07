using System.Collections.Generic;

using JOIEnergy.Domain;
using JOIEnergy.Generator;

namespace JOIEnergy.Data
{
    public static class SeedData
    {
        private const string MOST_EVIL_PRICE_PLAN_ID = "price-plan-0";
        private const string RENEWABLES_PRICE_PLAN_ID = "price-plan-1";
        private const string STANDARD_PRICE_PLAN_ID = "price-plan-2";

        public static Dictionary<string, string> GetSmartMeterToPricePlanMappings()
        {
            return new Dictionary<string, string>
            {
                ["smart-meter-0"] = MOST_EVIL_PRICE_PLAN_ID,
                ["smart-meter-1"] = RENEWABLES_PRICE_PLAN_ID,
                ["smart-meter-2"] = MOST_EVIL_PRICE_PLAN_ID,
                ["smart-meter-3"] = STANDARD_PRICE_PLAN_ID,
                ["smart-meter-4"] = RENEWABLES_PRICE_PLAN_ID
            };
        }

        public static List<PricePlan> GetPricePlans()
        {
            return [
                new PricePlan{
                    PlanName = MOST_EVIL_PRICE_PLAN_ID,
                    EnergySupplier = Enums.Supplier.DrEvilsDarkEnergy,
                    UnitRate = 10m,
                    PeakTimeMultiplier = new List<PeakTimeMultiplier>()
                },
                new PricePlan{
                    PlanName = RENEWABLES_PRICE_PLAN_ID,
                    EnergySupplier = Enums.Supplier.TheGreenEco,
                    UnitRate = 2m,
                    PeakTimeMultiplier = new List<PeakTimeMultiplier>()
                },
                new PricePlan{
                    PlanName = STANDARD_PRICE_PLAN_ID,
                    EnergySupplier = Enums.Supplier.PowerForEveryone,
                    UnitRate = 1m,
                    PeakTimeMultiplier = new List<PeakTimeMultiplier>()
                }
            ];
        }

        public static Dictionary<string, List<ElectricityReading>> GenerateInitialReadings(int readingsPerMeter = 20)
        {
            var meterMappings = GetSmartMeterToPricePlanMappings();
            var readings = new Dictionary<string, List<ElectricityReading>>();
            var generator = new ElectricityReadingGenerator();

            foreach (var smartMeterId in meterMappings.Keys)
            {
                readings[smartMeterId] = generator.Generate(readingsPerMeter);
            }
            return readings;
        }
    }
}
