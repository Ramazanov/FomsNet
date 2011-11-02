using System;
using Foms.CoreDomain.Contracts.Loans.Installments;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests
{
    public class DateCalculationStrategy
    {
        public static bool DateCalculationDiary(DateTime pLastDate, DateTime pDate)
        {
            return pLastDate.Year < pDate.Year
                        || (pLastDate.Year == pDate.Year && pLastDate.Month < pDate.Month)
                        || (pLastDate.Year == pDate.Year && pLastDate.Month == pDate.Month && pLastDate.Day < pDate.Day);
        }

        public static bool DateCalculationWeekly(DateTime pLastDate, DateTime pDate, int pWeekEndDay2)
        {
            int weekEndDay2 = pWeekEndDay2 == 6 ? 1 : pWeekEndDay2 + 1;

            int numbersOfDayToNextPostingDate = (weekEndDay2 <= (int)pLastDate.DayOfWeek) ? 7 - ((int)pLastDate.DayOfWeek - (int)weekEndDay2)
                : (int)weekEndDay2 - (int)pLastDate.DayOfWeek;

            return pDate.Subtract(pLastDate).Days >= numbersOfDayToNextPostingDate;
        }

        public static bool DateCalculationMonthly(DateTime pLastDate, DateTime pDate)
        {
            return pLastDate.Year < pDate.Year
                        || (pLastDate.Year == pDate.Year && pLastDate.Month < pDate.Month);
        }

        public static bool DateCalculationYearly(DateTime pLastDate, DateTime pDate)
        {
            return pLastDate.Year < pDate.Year;
        }

        public static DateTime GetNextWeekly(DateTime pDate, int pWeekEndDay2)
        {
            int weekEndDay2 = pWeekEndDay2 == 6 ? 1 : pWeekEndDay2 + 1;

            return pDate.AddDays((weekEndDay2 <= (int)pDate.DayOfWeek)
                   ? 7 - ((int)pDate.DayOfWeek - weekEndDay2)
                   : weekEndDay2 - (int)pDate.DayOfWeek);
        }

        public static DateTime GetNextMaturity(DateTime pDate, InstallmentType pPeriodicity, int pNumberPeriods)
        {
            for (int i = 0; i < pNumberPeriods; i++)
            {
                pDate = pDate.AddMonths(pPeriodicity.NbOfMonths).AddDays(pPeriodicity.NbOfDays);
            }

            return pDate;
        }

        public static DateTime GetLastMaturity(DateTime pDate, InstallmentType pPeriodicity, int pNumberPeriods)
        {
           for (int i = 0; i < pNumberPeriods; i++)
            {
                pDate = pDate.AddMonths(-pPeriodicity.NbOfMonths).AddDays(-pPeriodicity.NbOfDays);
            }

           return pDate;
        }
    }
}
