using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using SolverCore.Solvers;
using SolverCore.Methods;
using System.Collections;


namespace Extensions
{
    public class ProxyMethod : IMethod
    {
        IMethod method;
        
        public int[] MultCount { get; private set; } = new int[2] { 0, 0 };

        public IVector x => method.x;

        public ProxyMethod(IMethod method)
        {
            Counters.ResetAll();
            this.method = method;
        }

        public bool InitMethod(ILinearOperator A, IVector x0, IVector b, bool malloc = false)
        {
            Counters.Reset("Mult");

            var result = method.InitMethod(A, x0, b, false);

            MultCount[0] = Counters.GetCount("Mult"); 
            Counters.ResetAll();

            return result;
        }

        public void MakeStep(out int iter, out double residual)
        {
            method.MakeStep(out iter, out residual);
            if(iter == 1)
                MultCount[1] = Counters.GetCount("Mult");
        }

        bool IMethod.InitMethod(ILinearOperator A, IVector x0, IVector b, bool malloc)
        {
            return method.InitMethod(A, x0, b, malloc);
        }
    }


}


