using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class TimePowerDb : IFeature
    {
        #region IFeature Members

        public string Description
        {
            get { return "Computes signal power in dB"; }
        }

        public double Feature { get; private set; }
        public IDataProvider DataProvider { get; set; }

        public void Compute()
        {
            List<double> Data = DataProvider.Data;
            double energy = 0;
            foreach (double b in Data)
            {
                double d = Math.Abs(b);
                energy += d * d;
            }
            Feature = Math.Log(energy / Data.Count);
        }

        #endregion

        
        public TimePowerDb(IDataProvider DataProvider)
        {
            this.DataProvider = DataProvider;
        }
    }
}
