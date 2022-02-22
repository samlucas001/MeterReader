using MeterReader.Data;
using MeterReader.MeterReading.Model;
using MeterReader.Models;

namespace MeterReader.MeterReading
{
    public class MeterReadUploadDAO : IMeterReadUploadDAO
    {
        private readonly MeterReaderContext _context;

        public MeterReadUploadDAO(MeterReaderContext context)
        {
            _context = context;
        }

        public void UploadData(List<MeterReadingWithError> meterReadings)
        {
            try
            {
                //get the list of readings and only get the ones with no errors
                foreach (var mr in meterReadings.Where(x => x.HasError == false))
                {
                    //does this account exist in the databse?
                    var account = _context.Account.Where(x => x.AccountId == mr.AccountId).FirstOrDefault();

                    if (account == null)
                    {
                        //no account add an error for reporting
                        mr.HasError = true;
                        mr.ErrorMessage = $"AccountId {mr.AccountId} is not a valid account";
                    }
                    else
                    {
                        //account exists in the accounts table
                        //check if the data is in the db and not the same
                        var entry = (from mrd in _context.MeterReadingData
                                     where
                                         mrd.AccountId == mr.AccountId &&
                                         mrd.DateTimeEntered == mr.DateTimeEntered &&
                                         mrd.ValueEntered == mr.ValueEntered
                                     select mrd).FirstOrDefault();

                        if (entry != null)
                        {
                            //we already have a meter reading of the same vale, add an error
                            mr.HasError = true;
                            mr.ErrorMessage = $"Accountid {mr.AccountId} has already had the same entry entered";

                        }
                        else
                        {
                            //check if the new read is newer than the old one
                            var ageEntry = (from mrd in _context.MeterReadingData
                                            where
                                                mrd.AccountId == mr.AccountId &&
                                                mrd.DateTimeEntered > mr.DateTimeEntered
                                            select mrd).FirstOrDefault();

                            if (ageEntry != null)
                            {
                                //we have a reading that is older than one in the DB
                                mr.HasError = true;
                                mr.ErrorMessage = $"there is a meter reading that is new than the one entered for AccountId {mr.AccountId}";
                            }
                            else
                            {
                                //we can finally add an entry
                                MeterReadingData mrd = new MeterReadingData
                                {
                                    AccountId = mr.AccountId,
                                    DateTimeEntered = mr.DateTimeEntered,
                                    ValueEntered = mr.ValueEntered
                                };
                                _context.MeterReadingData.Add(mrd);
                                _context.SaveChanges();
                            }
                        }
                    }
                

                //var entry = (from acc in _context.Account
                //             join mrd in _context.MeterReadingData on acc.AccountId equals mrd.AccountId
                //             where acc.AccountId == mr.AccountId
                //             select mrd).ToList();


                //if (entry != null)
                //{
                //    //we have a valid accountid
                //    //check all the data is not the same vlaue and date

                //}
                //else
                //{ 
                //    //throw error as this account does not exist
                //}


            }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            //var people = _context.Account.ToList();
        }

    }
}
