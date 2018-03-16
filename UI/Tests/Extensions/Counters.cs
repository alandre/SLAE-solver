using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
        public static void ResetAll()
        {
            Type type = typeof(Counters);
            foreach (var field in type.GetFields())
            {
                var curField = field.GetValue(type);
                if (curField is Counter)
                    (curField as Counter).ResetCount();
            }
        }
        public static Counter Mult = new Counter();
        public static Counter LMult = new Counter();
        public static Counter UMult = new Counter();

    }
}
