using MeterReader.MeterReading;
using MeterReader.MeterReading.Model;

namespace MeterReader.Models
{
    public class MeterReadUploadBO : IMeterReadUploadBO
    {
        private readonly IMeterReadUploadDAO _meterReadUploadDAO;

        public MeterReadUploadBO(IMeterReadUploadDAO meterReadUploadDAO)
        {
            _meterReadUploadDAO = meterReadUploadDAO;
        }

        public List<MeterReadingWithError> UploadMeterReading(string meterFile, bool firstRowHeaders)
        {
            //split the file, and skip first line if needed
            var splitText = meterFile.Split("\r\n").Skip(Convert.ToInt32(firstRowHeaders));

            var counter = 1;
            List<string> errors = new List<string>();
            List<MeterReadingWithError> meterReadings = new List<MeterReadingWithError>();

            //want to split this out to a "test" datatypes, was thinking of adding this to some helper class as i repeated myself twice 
            //but wanted detaile errors, and started to get pressed for tuime

            foreach (var v in splitText)
            {
                var values = v.Split(',');

                MeterReadingWithError mr = new MeterReadingWithError();

                int iValue = 0;
                var dValue = new DateTime();

                //check if there is errors on this line - got caught out with there being a blank entry on the last line
                if (string.IsNullOrEmpty(values[0]))
                {
                    mr.HasError = true;
                    mr.ErrorMessage += $"No data at line {counter}";
                }
                else
                {
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
                }

                meterReadings.Add(mr);
                counter++;
            }


            //there is now a list of legit (and errored) readings, send them to the data object layer to get consumed
            _meterReadUploadDAO.UploadData(meterReadings);

            return meterReadings;
        }

    }

}
