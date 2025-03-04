using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Util
{
    public static class Dates
    {

        public static DateTime Peru(this DateTime date, string timeZone)
        {
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            DateTime cstTime = TimeZoneInfo.ConvertTime(date, cstZone);
            return cstTime;
        }
    }
}
