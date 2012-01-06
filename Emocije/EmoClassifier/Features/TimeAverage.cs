using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class TimeAverage : IFeature
    {
        #region IFeature Members

        public string Description
        {
            get { return "Averages"; }
        }

        public double Feature
        {
            get; 
            private set;
        }

        public IDataProvider DataProvider { get; set; }

        public void Compute()
        {
            List<double> Data = DataProvider.Data;
            foreach (double d in Data)
            {
                Feature += d;
            }
            Feature = Feature / Data.Count();
        }

        #endregion


        public TimeAverage(IDataProvider DataProvider  )
        {
            this.DataProvider = DataProvider;
        }
    }
}
