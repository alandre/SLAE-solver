using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace SolverCore
{
    public interface ILogger
    {
        //Наброски [УБРАТЬ Остались для сбоки проекта]
        void write();
        void read();

        /// <summary>
        /// Запись в лог
        /// </summary>
        /// <param name="Iter">текущая итерация</param>
        /// <param name="Residual">текущая невязка</param>
        void Write(int Iter, double Residual);
        //void Read(int Iter, double Residual);
        KeyValuePair<int, double> Read();
        ImmutableList<double> GetList();
    }
}
