using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HelperMethods
{
    public static class TimeHelper
    {
        public static string CalculateTimeAgoInArabic(DateTime timestamp)
        {
            var timeSpan = DateTime.UtcNow - timestamp;

            if (timeSpan.TotalMinutes < 1)
                return "الآن";
            else if (timeSpan.TotalMinutes < 60)
            {
                var minutes = (int)timeSpan.TotalMinutes;
                return minutes == 1 ? "منذ دقيقة" : $"منذ {minutes} دقائق";
            }
            else if (timeSpan.TotalHours < 24)
            {
                var hours = (int)timeSpan.TotalHours;
                return hours == 1 ? "منذ ساعة" : $"منذ {hours} ساعات";
            }
            else if (timeSpan.TotalDays < 7)
            {
                var days = (int)timeSpan.TotalDays;
                return days == 1 ? "منذ يوم" : $"منذ {days} أيام";
            }
            else if (timeSpan.TotalDays < 30)
            {
                var weeks = (int)(timeSpan.TotalDays / 7);
                return weeks == 1 ? "منذ أسبوع" : $"منذ {weeks} أسابيع";
            }
            else if (timeSpan.TotalDays < 365)
            {
                var months = (int)(timeSpan.TotalDays / 30);
                return months == 1 ? "منذ شهر" : $"منذ {months} أشهر";
            }
            else
            {
                var years = (int)(timeSpan.TotalDays / 365);
                return years == 1 ? "منذ سنة" : $"منذ {years} سنوات";
            }
        }
    }
}

