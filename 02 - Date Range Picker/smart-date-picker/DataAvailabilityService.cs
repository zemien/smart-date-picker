using Sodium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace smart_date_picker
{
    public class DataAvailabilityService
    {
        private readonly CellSink<DateRange> mDataAvailabilitySink;

        /// <summary>
        /// Initializes a Data Availability Service
        /// </summary>
        public DataAvailabilityService()
        {
            mDataAvailabilitySink = new CellSink<DateRange>(new DateRange(new DateTime(2014, 1, 1), new DateTime(2016, 07, 31)));
        }

        /// <summary>
        /// Dates that we can run reports for
        /// </summary>
        public Cell<DateRange> DataAvailability
        {
            get
            {
                return mDataAvailabilitySink;
            }
        }

        /// <summary>
        /// Get data availability dates. This will also send() on the cell.
        /// </summary>
        /// <returns></returns>
        public async Task<DateRange> GetDataAvailability()
        {
            //Simulate I/O latency
            Thread.Sleep(500);
            mDataAvailabilitySink.Send(new DateRange(new DateTime(2014, 1, 1), new DateTime(2016, 12, 31)));
            return mDataAvailabilitySink.Sample();
        }
    }
}
