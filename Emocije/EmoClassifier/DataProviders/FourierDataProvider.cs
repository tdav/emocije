using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.DataProviders
{
    public class FourierDataProvider:IDataProvider
    {

        #region IDataProvider Members

        private List<double> _data = new List<double>();

        public List<double> Data
        {
            get { return _data; }
            set
            {
                this._data = value;
                NormalizeData();
                CalculateFourierTransform();
            }
        }

        #endregion

        public List<double> PowerSpectra = new List<double>();
        LomontFFT fft = new LomontFFT();
            
        private void CalculateFourierTransform()
        {
            List<double> D = Data.Take<double>(1024).ToList();
            while (D.Count() < 1024)
                D.Add(0);
            double[] dat = D.ToArray();
            fft.RealFFT(dat, true);
            PowerSpectra.Add(dat[0] * dat[0]);
            for (int i = 2; i < dat.Length; i+=2)
            {
                PowerSpectra.Add(dat[i] * dat[i] + dat[i + 1] * dat[i + 1]);
            }
            PowerSpectra.Add(dat[1] * dat[1]);
        }

        private void NormalizeData()
        {
            double min = _data.Min();
            double max = _data.Max();
            double diff = max-min;
            List<double> newData = new List<double>();
            foreach (double d in _data)
            {
                newData.Add((((d - min) / diff) - 0.5) * 2);
            }
            _data = newData;
        }

    }
}
