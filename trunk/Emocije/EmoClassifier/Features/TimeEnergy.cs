using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class TimeEnergy:IFeature
    {
        #region IFeature Members

        public string Description
        {
            get { return "Energy"; }
        }

        public double Feature { get; private set; }

        public IDataProvider DataProvider { get; set; }

        public void Compute()
        {
            List<double> Data = DataProvider.Data;
            double res = 0;
            foreach (double b in Data)
            {
                double d = Math.Abs(b);
                res += d*d;
            }
            Feature = res;
        }

        #endregion

        public TimeEnergy(IDataProvider DataProvider)
        {
            this.DataProvider = DataProvider;
        }
    }
}
