using MeterReader.MeterReading;
using MeterReader.MeterReading.Model;

namespace MeterReader.Models
{
    public class MeterReadUploadBO : IMeterReadUploadBO
    {
        public List<MeterReadingWithError> UploadMeterReading(string meterFile, bool firstRowHeaders)
        {
            var splitText = meterFile.Split("\r\n").Skip(Convert.ToInt32(firstRowHeaders));

            var counter = 1;
            List<string> errors = new List<string>();
            List<MeterReadingWithError> meterReadings = new List<MeterReadingWithError>();

            //want to split this out to a "test" datatypes
            foreach (var v in splitText)
            {
                var values = v.Split(',');

                MeterReadingWithError mr = new MeterReadingWithError();

                int iValue = 0;
                var dValue = new DateTime();

                if (int.TryParse(values[0], out iValue))
                {
                    mr.AccountId = iValue;
                }
                else
                {
                    mr.HasError = true;
                    mr.ErrorMessage += $"Error at line {counter} for accountId expexted int but got '{ values[0] }'";
                }

                if (DateTime.TryParse(values[1], out dValue))
                {
                    mr.DateTimeEntered = dValue;
                }
                else
                {
                    mr.HasError = true;
                    mr.ErrorMessage += $"Error at line {counter} for reading date time expexted date time but got '{ values[1] }'";
                }

                if (int.TryParse(values[2], out iValue))
                {
                    mr.ValueEntered = iValue;
                }
                else
                {
                    mr.HasError = true;
                    mr.ErrorMessage += $"Error at line {counter} for read value expexted int but got '{ values[2] }'";
                }

                meterReadings.Add(mr);
                counter++;
            }

            //call db thing whcihj will retirn errors, concat the erroir list anbd return back



            return meterReadings;

        }

    }
}
