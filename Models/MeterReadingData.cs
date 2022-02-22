using System.ComponentModel.DataAnnotations;

namespace MeterReader.Models
{
    public class MeterReadingData
    {
        
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime DateTimeEntered { get; set; }
        public int ValueEntered { get; set; }
    }
}
