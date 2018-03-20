using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using SolverCore;

namespace UI
{
    static class IO
    {
        public static void writeSLAE(SLAE slae, string filename)
        {
            var data = JsonConvert.SerializeObject(slae);

            File.WriteAllText(filename, data);
        }
    }
}
