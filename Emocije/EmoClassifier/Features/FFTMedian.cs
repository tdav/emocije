using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class FFTMedian : IFeature
    {
        #region IFeature Members

        public string Description
        {
            get { return "Computes Median"; }
        }

        public double Feature { get; private set; }
        public IDataProvider DataProvider { get; set; }

        public void Compute()
        {
            List<double> Data = DataProvider.PowerSpectra;
            double median = 0;

            Data.Sort();
            int middle = Data.Count / 2;

            if (Data.Count % 2 != 0)
                median = Data[middle];
            else
                median = (Data[middle] + Data[middle - 1]) / 2;

            Feature = median;
        }

        #endregion


        public FFTMedian(IDataProvider DataProvider)
        {
            this.DataProvider = DataProvider;
        }
    }
}
