using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class Std : IFeature
    {
        #region IFeature Members

        public string Description
        {
            get { return "Computes standard deviation"; }
        }

        public double Feature { get; private set; }
        public IDataProvider DataProvider { get; set; }

        public void Compute()
        {
            List<double> Data = DataProvider.Data;
            double average = Data.Average();
            double sum = 0.0;
            foreach (double b in Data)
            {

                sum += Math.Pow((b - average), 2);
            }
            int n = Data.Count;
            Feature = Math.Sqrt(sum / (n - 1));
        }

        #endregion
    }
}
