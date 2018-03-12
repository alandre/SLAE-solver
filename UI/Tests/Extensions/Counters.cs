using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    internal class Counter
    {
        public int count { get; private set; } = 0;

        public void ResetCount()
        {
            count = 0;
        }

        public void Inc()
        {
            count++;
        }
    }
    internal static class Counters
    {
        public static Counter Mult = new Counter();

    }
}
