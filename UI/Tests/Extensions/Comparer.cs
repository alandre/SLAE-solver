using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class Comparer : IEqualityComparer<(double, int, int)>
    {
        public bool Equals((double, int, int) x, (double, int, int) y)
        {
            return ((x.Item1 == y.Item1) &&
                    (x.Item2 == y.Item2) &&
                    (x.Item3 == y.Item3));
        }

        public int GetHashCode((double, int, int) obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
