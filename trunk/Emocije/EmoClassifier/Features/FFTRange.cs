using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class FFTRange : IFeature
    {
        #region IFeature Members

        public string Description
        {
            get { return "Computes range"; }
        }

        public double Feature { get; private set; }
        public IDataProvider DataProvider { get; set; }

        public void Compute()
        {
            List<double> Data = DataProvider.PowerSpectra;
            Feature = Data.Max() - Data.Min();
        }

        #endregion


     public FFTRange(IDataProvider DataProvider  )
        {
            this.DataProvider = DataProvider;
        }
    }
}
