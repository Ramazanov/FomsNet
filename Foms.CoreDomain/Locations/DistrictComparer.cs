using System.Collections.Generic;

namespace Foms.CoreDomain.Locations
{
    public class DistrictComparer : IComparer<District>
    {
        public int Compare(District x, District y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
