using System;
using System.Collections.Generic;
using System.Linq;

using JOIEnergy.Domain;
using JOIEnergy.Repositories;

namespace JOIEnergy.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly IMeterReadingRepository _meterReadingRepository;
        public MeterReadingService(IMeterReadingRepository meterReadingRepository)
        {
            _meterReadingRepository = meterReadingRepository;
        }

        public List<ElectricityReading> GetReadings(string smartMeterId)
        {
            return _meterReadingRepository.GetReadings(smartMeterId);
        }

        public List<ElectricityReading> GetLastWeekReadings(string smartMeterId)
        {
            var lastWeek = DateTime.UtcNow.AddDays(-7);
            var readings = _meterReadingRepository.GetReadings(smartMeterId);
            return readings.Where(reading => reading.Time >= lastWeek).ToList();
        }

        public void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings)
        {
            _meterReadingRepository.StoreReadings(smartMeterId, electricityReadings);
        }
    }
}
