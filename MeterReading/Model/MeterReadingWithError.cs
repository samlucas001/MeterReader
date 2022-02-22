namespace MeterReader.MeterReading.Model
{
    public class MeterReadingWithError : MeterReader.Models.MeterReadingData
    {
        public bool HasError { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
