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
        public DateRange GetDataAvailability()
        {
            //Simulate I/O latency
            Thread.Sleep(500);
            return new DateRange(new DateTime(2014, 1, 1), new DateTime(2016, 12, 31));
        }
    }
}
