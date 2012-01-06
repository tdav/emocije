using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace EmoClassifier.Testing
{
    public class TestResult
    {
        public bool Success{get;set;}
        public double AbsoluteDifference { get; set; }
        
    }
    /// <summary>
    /// Za testiranje izračuna referentnih značajki i značajki koje računa ova biblioteka
    /// </summary>
    public class TestManager
    {
        /// <summary>
        /// Test metoda. Potrebne su dvije ulazne datoteke datoteke, InputFile i OutputFile. U jednoj su
        /// ulazni podatci za značajku, a u drugoj referentni podatci (izlaz iz Matlaba).
        /// Ulazni podatci i izlaz i matlaba se spremaju koristeći sljedeću sintaksu:
        /// save('ime_datoteke.txt','varijabla','-ascii','-double','-tabs');
        /// </summary>
        /// <param name="InputFile">Datoteka s ulaznim podatcima</param>
        /// <param name="OutputFile">Datoteka s izlaznim podatcima</param>
        /// <param name="DataProvider">DataProvider za IFeature</param>
        /// <param name="Feature">Značajka</param>
        /// <param name="SubWindowLength">Dužina prozora u uzorcima</param>
        /// <param name="SubWindowShift">Dužina pomaka u uzorcima</param>
        /// <returns></returns>
        public static TestResult ExecuteTest(string InputFile, string OutputFile, IDataProvider DataProvider, IFeature Feature, int SubWindowLength, int SubWindowShift)
        {

            List<double> InputData = ReadFromFile(InputFile);
            List<double> OutputData = ReadFromFile(OutputFile);

            FastQueue<double> Data = new FastQueue<double>(100000);
            List<double> Result = new List<double>();

            Data.Enqueue(InputData.ToArray());

            while (Data.Count > SubWindowLength)
            {
                List<double> CurrentData;

                CurrentData = Data.Peek(SubWindowLength);
                Data.Delete(SubWindowShift);
                DataProvider.Data = CurrentData;

                Feature.Compute();
                Result.Add(Feature.Feature);


            }

            double absdiff = 0;
            for (int i = 0; i < Result.Count || i < OutputData.Count; i++)
            {
                absdiff += Math.Abs(Result[i] - OutputData[i]);
            }

            TestResult tr = new TestResult();
            tr.AbsoluteDifference = absdiff;
            if (absdiff < 100)
                tr.Success = true;
            else
                tr.Success = false;
            

            return tr;
        }

        private static List<double> ReadFromFile(string file)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            StreamReader reader = new StreamReader(file);
            List<double> Data = new List<double>();
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                double data;

                if (double.TryParse(line,NumberStyles.Float,culture, out data))
                {
                    Data.Add(data);
                }
            }
            return Data;
        }
    }
}
