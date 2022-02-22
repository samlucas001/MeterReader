using MeterReader.MeterReading.Model;
using MeterReader.Models;

namespace MeterReader.MeterReading
{
    /// <summary>
    /// Requirements for the meter reader upload Data Access Object
    /// </summary>
    public interface IMeterReadUploadDAO
    {
        /// <summary>
        /// Upload passed readings to database
        /// </summary>
        public void UploadData(List<MeterReadingWithError> meterReadings);


    }
}
