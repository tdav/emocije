using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EmoClassifier.Features
{
    public class FFTMaximum : IFeature
    { 

        #region IFeature Members

        public string Description
        {
            get { return "Maximum value"; }
       }

        public double Feature
        {
            get;
            private set;
        }

        public IDataProvider DataProvider { get; set; }

        public void Compute()
        {
            List<double> Data = DataProvider.PowerSpectra;
            Feature = Data.Max();
        }

        #endregion

        public FFTMaximum(IDataProvider DataProvider  )
        {
            this.DataProvider = DataProvider;
        }
    }
}
