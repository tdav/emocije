using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class Minimum : IFeature
    {

        #region IFeature Members

        public string Description
        {
            get { return "Minimum value"; }
        }

        public double Feature { get; private set; }
        public IDataProvider DataProvider { get; set; }

        public void Compute()
        {
            List<double> Data = DataProvider.Data;
            Feature = Data.Min();
        }

        #endregion


        public Minimum(IDataProvider DataProvider)
        {
            this.DataProvider = DataProvider;
        }

    }
}
