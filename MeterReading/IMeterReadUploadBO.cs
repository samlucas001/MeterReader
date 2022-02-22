using MeterReader.MeterReading.Model;

namespace MeterReader.MeterReading
{
    /// <summary>
    /// Business object for handling meter reading upload
    /// </summary>
    public interface IMeterReadUploadBO
    {
        /// <summary>
        /// Uploads file and splits out to a meterReading class, add check for if first row is headers
        /// </summary>
        /// <param name="meterFile"></param>
        /// <param name="firstRowHeaders"></param>
        public List<MeterReadingWithError> UploadMeterReading(string meterFile, bool firstRowHeaders);
    }
}
