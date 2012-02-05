using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class Changerate : IFeature
    {
        #region IFeature Members

        public string Description
        {
            get { return "Computes changerate"; }
        }

        public double Feature { get; private set; }
        public IDataProvider DataProvider { get; set; }

        public void Compute()
        {
            List<double> Data = DataProvider.Data;
            int n = Data.Count;

            double[] absDiff = new double [n - 1];
            
            for (int i = 0; i < n-1; i++)
            {
                absDiff[i] = Math.Abs(Data[i + 1] - Data[i]);
            }

            Feature = absDiff.Average();
        }

        #endregion

    }
}
