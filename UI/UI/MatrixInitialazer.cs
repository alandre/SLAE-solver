using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace UI
{
    public class MatrixInitialazer
    {
        public int[] column { get; set; }
        public int[] row { get; set; }

        public double[,] dense { get; set; }
        public double[][] denseL { get; set; }

        public int[] ia { get; set; }
        public int[] ja { get; set; }

        public double[] gg { get; set; }
        public double[] al { get; set; }
        public double[] au { get; set; }

        public double[] di { get; set; }

        public double[] x0 { get; set; }

        public bool symmetry { get; set; }

        public int size { set; get; }

        public double[] b { get; set; }

        public static MatrixInitialazer Input(string data, bool symmetry)
        {
            var matrix = JsonConvert.DeserializeObject<MatrixInitialazer>(data);
            matrix.symmetry = symmetry;
            return matrix;
        }

        public static MatrixInitialazer InputB(string data, bool symmetry)
        {
            byte[] tmp = Convert.FromBase64String(data);
            MemoryStream ms = new MemoryStream(tmp);
            using (BsonReader reader = new BsonReader(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                var matrix = serializer.Deserialize<MatrixInitialazer>(reader);
                matrix.symmetry = symmetry;
                return matrix;
            }
        }
    }   
}
