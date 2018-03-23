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
        public int Сount { get; private set; } = 0;

        public void ResetCount()
        {
            Сount = 0;
        }

        public void Inc()
        {
            Сount++;
        }
    }
    internal static class Counters
    {
        private static Dictionary<string, Counter> CountersDictionary;

        static Counters()
        {
            CountersDictionary = new Dictionary<string, Counter>();
            CountersDictionary.Add("Mult", new Counter());
            CountersDictionary.Add("LMult", new Counter());
            CountersDictionary.Add("UMult", new Counter());

        }
        public static void ResetAll()
        {
            foreach (var counter in CountersDictionary)
            {
                counter.Value.ResetCount();
            }
        }

        public static void Inc(string name)
        {
            CountersDictionary[name].Inc();
        }

        public static void Reset(string name)
        {
            CountersDictionary[name].ResetCount();
        }
        public static int GetCount(string name)
        {
            return CountersDictionary[name].Сount;
        }


    }
}
