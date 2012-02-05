using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Features
{
    public class MFCC:IFeature
    {
        [System.Runtime.InteropServices.DllImport(@"PitchDLL.dll")]
        static extern double GetCoefficient(double[] spectralData, System.UInt32 samplingRate, System.UInt32 NumFilters, System.UInt32 binSize, System.UInt32 m);

        #region IFeature Members

        public string Description
        {
            get { return "MFCC " + Coefficient.ToString(); }
        }

        public double Feature
        {
            get;
            private set;
        }

        public void Compute()
        {
            double c = GetCoefficient(DataProvider.Data.ToArray(), (uint)DataProvider.SamplingFrequency, 24, (uint)DataProvider.Data.Count(), this.Coefficient);
            this.Feature = c;
        }

        public IDataProvider DataProvider
        {
            get;
            set;
        }

        #endregion

        public uint Coefficient { get; set; }

        public MFCC(IDataProvider DataProvider, uint Coefficient)
        {
            this.DataProvider = DataProvider;
            this.Coefficient = Coefficient;
        }

    }
}
