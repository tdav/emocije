using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class ZeroCrossingRate:IFeature
    {
        #region IFeature Members

        public string Description
        {
            get { return "Zero crossing rate"; }
        }

        public double Feature
        {
            get;
            private set;
        }

        public void Compute()
        {
            List<double> Data = DataProvider.Data;
            double ZCR = 0;
            for (int i = 1; i < Data.Count - 1; i++)
            {
                if (Data[i] * Data[i - 1] < 0)
                    ZCR++;
            }
            Feature = ZCR /  (Data.Count-1);
        }

        public IDataProvider DataProvider { get; set; }
        
        #endregion

        

        public ZeroCrossingRate(IDataProvider DataProvider)
        {
            this.DataProvider = DataProvider;
        }
    }
}
