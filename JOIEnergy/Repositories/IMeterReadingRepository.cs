using System.Collections.Generic;
using System.Linq;

using JOIEnergy.Data;
using JOIEnergy.Domain;

namespace JOIEnergy.Repositories
{
    public interface IMeterReadingRepository
    {
        List<ElectricityReading> GetReadings(string smartMeterId);
        void StoreReadings(string smartMeterId, List<ElectricityReading> readings);
        bool HasReadings(string smartMeterId);
    }
    public class InMemoryMeterReadingRepository : IMeterReadingRepository
    {
        private readonly Dictionary<string, List<ElectricityReading>> _readings;

        public InMemoryMeterReadingRepository()
        {
            _readings = SeedData.GenerateInitialReadings();
        }

        public List<ElectricityReading> GetReadings(string smartMeterId)
        {
            return _readings.TryGetValue(smartMeterId, out var readings)
                ? readings
                : new List<ElectricityReading>();
        }

        public void StoreReadings(string smartMeterId, List<ElectricityReading> readings)
        {
            if (_readings.ContainsKey(smartMeterId))
            {
                _readings[smartMeterId].AddRange(readings);
            }
            else
            {
                _readings[smartMeterId] = readings;
            }
        }

        public bool HasReadings(string smartMeterId)
        {
            return _readings.ContainsKey(smartMeterId) && _readings[smartMeterId].Any();
        }
    }
}
