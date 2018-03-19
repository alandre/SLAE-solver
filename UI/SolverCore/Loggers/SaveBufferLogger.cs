using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Immutable;


namespace SolverCore.Loggers
{
    public class SaveBufferLogger : ILogger
    {
        
        
        ImmutableList<double> LogList = ImmutableList.CreateRange(new double[0] { });


        public void read ()

        {
            throw new NotImplementedException();
        }
        //Удалить
        public KeyValuePair<int,double> Read()
        {
            if (!LogList.IsEmpty)
            {
                int Count = LogList.Count();
                double r = LogList[Count-1];
                var newEntry = new KeyValuePair<int, double>(Count-1, r);
                return newEntry;
            }
            else
            {
                var newEntry = new KeyValuePair<int, double>(0, 0);
                return newEntry;
            }
        }
        public ImmutableList<double> GetList ()
        {
            return LogList;
        }
        public void write()
        {
            throw new NotImplementedException();
        }
        
        public void Write(int Iter, double Residual)
        {
            LogList=LogList.Add(Residual);

            // throw new NotImplementedException();
        }

    }
}
