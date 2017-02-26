using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.Common
{
    public static class DateTimeExtensions
    {
        #region DayOfWeek
        public static int WeekNumber(DateTime date)
        {
            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
        public static DayOfWeekModel GetDayOfWeek(DateTime dateTime)
        {
            DayOfWeekModel dayOfWeek = new DayOfWeekModel();
            dayOfWeek.Monday = dateTime.AddDays(0).ToString("dd/MM/yyyy");
            dayOfWeek.Tuesday = dateTime.AddDays(1).ToString("dd/MM/yyyy");
            dayOfWeek.Wednesday = dateTime.AddDays(2).ToString("dd/MM/yyyy");
            dayOfWeek.Thursday = dateTime.AddDays(3).ToString("dd/MM/yyyy");
            dayOfWeek.Friday = dateTime.AddDays(4).ToString("dd/MM/yyyy");
            dayOfWeek.Saturday = dateTime.AddDays(5).ToString("dd/MM/yyyy");
            dayOfWeek.Sunday = dateTime.AddDays(6).ToString("dd/MM/yyyy");
            return dayOfWeek;
        }
        public static string StringDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            DateTime firtWeek = firstWeekDay.AddDays(weekOfYear * 7).AddDays(1);
            DateTime lastWeek = firtWeek.AddDays(6);
            return firtWeek.ToString("dd/MM/yyyy") + " - " + lastWeek.ToString("dd/MM/yyyy");
        }
        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7).AddDays(1);
        }

        #endregion

        #region WeekOfYear
        public static int WeeksInYear(int year)
        {
            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return cal.GetWeekOfYear(new DateTime(year, 12, 28), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static List<MonthOfYearModel> GetMonthOfYear(int year)
        {
            List<MonthOfYearModel> listMonthOfYearModel = new List<MonthOfYearModel>();
            MonthOfYearModel monthOfYearModel = null;
            for (int i = 1; i <= 12; i++)
            {
                monthOfYearModel = new MonthOfYearModel();
                var firstDayOfMonth = new DateTime(year, i, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                monthOfYearModel.NameMonth = firstDayOfMonth.ToString("dd/MM/yyyy") + " - " + lastDayOfMonth.ToString("dd/MM/yyyy");
                monthOfYearModel.CodeMonth = i;
                listMonthOfYearModel.Add(monthOfYearModel);
            }
            return listMonthOfYearModel;
        }
        public static List<WeekOfYearModel> GetWeekOfYear()
        {
            int week = WeeksInYear(DateTime.Now.Year);
            int curNumberWeek = WeekNumber(DateTime.Now);
            List<WeekOfYearModel> listWeekOfYearModel = new List<WeekOfYearModel>();
            for (int i = 1; i <= week; i++)
            {
                WeekOfYearModel weekOfYearModel = new WeekOfYearModel();
                weekOfYearModel.CodeWeek = i;
                weekOfYearModel.NameWeek = StringDateOfWeek(DateTime.Now.Year, i, System.Globalization.CultureInfo.CurrentCulture);

                //Check is current week
                weekOfYearModel.IsWeek = -1;
                if (i == curNumberWeek)
                {
                    weekOfYearModel.IsWeek = (int)WeekEnum.ThisWeek;
                }
                else if (i == curNumberWeek-1)
                {
                    weekOfYearModel.IsWeek = (int)WeekEnum.LastWeek;
                }
                else if (i == curNumberWeek+1)
                {
                    weekOfYearModel.IsWeek = (int)WeekEnum.NextWeek;
                }

                listWeekOfYearModel.Add(weekOfYearModel);
            }
            return listWeekOfYearModel;

        }
        public static int WeekOfYearISO8601(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        #endregion
        #region Convert DateTime
        public static DateTime ConvertDateTime(string dateTime)
        {
            DateTime myDate = DateTime.Now;
            try
            {
                myDate = DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {

                myDate = DateTime.Today;
            }
            return myDate;
        }
        #endregion
    }
}
