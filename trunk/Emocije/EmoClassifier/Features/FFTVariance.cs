using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class FFTVariance : IFeature
    {
        #region IFeature Members

        public string Description
        {
            get { return "Computes variance"; }
        }

        public double Feature { get; private set; }
        public IDataProvider DataProvider { get; set; }

        public void Compute() 
        {   
           
            List<double> Data = DataProvider.PowerSpectra;
            double average = Data.Average();
            double sum = 0.0;
            foreach(double b in Data)
            {
              
                sum += Math.Pow((b-average), 2);
            }
            int n = Data.Count;
            Feature = sum/(n-1);
        }

        #endregion

        public FFTVariance(IDataProvider DataProvider  )
        {
            this.DataProvider = DataProvider;
        }
    }
}
