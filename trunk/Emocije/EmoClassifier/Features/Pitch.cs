using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class Pitch:IFeature
    {
        [System.Runtime.InteropServices.DllImport(@"PitchDLL.dll")]
        static extern double getPitch(double[] samples, System.Int32 length);

        #region IFeature Members

        public string Description
        {
            get { return "Pitch"; }
        }

        public double Feature
        {
            get;
            private set;
        }

        public void Compute()
        {
            List<double> Data = DataProvider.Data;
            this.Feature = getPitch(Data.ToArray(), Data.Count());
        }

        public IDataProvider DataProvider
        {
            get;
            set;
        }

        #endregion

        public Pitch(IDataProvider DataProvider  )
        {
            this.DataProvider = DataProvider;
        }

    }
}
