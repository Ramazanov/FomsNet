using System;

namespace Foms.Shared
{
    /// <summary>
    /// Summary description for PublicHolidaysComparer.
    /// </summary>
    /// 
    class DateTimeComparer : System.Collections.IComparer
    {
        private bool _sortDescending = false;

        //pass true to the constructor to sort descending
        public DateTimeComparer(bool descending)
        {
            _sortDescending = descending;
        }

        public int Compare(object x, object y)
        {
            DateTime dateA = (DateTime)x;
            DateTime dateB = (DateTime)y;

            if (_sortDescending)
                return dateA.CompareTo(dateB);

            return dateB.CompareTo(dateA);
        }

        public int Compare(DateTime x, DateTime y)
        {
            throw new NotImplementedException();
        }
    }
}