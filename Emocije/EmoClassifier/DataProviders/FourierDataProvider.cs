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
                //NormalizeData();
                CalculateFourierTransform();
            }
        }

        public int SamplingFrequency { get; set; }
        #endregion

        public List<double> PowerSpectra = new List<double>();
        LomontFFT fft = new LomontFFT();
            
        private void CalculateFourierTransform()
        {
            int len=Data.Count();
            len = (int)Math.Pow(2,Math.Floor(Math.Log((double)len, 2))+1);
            List<double> D = Data.ToList();

            while (D.Count() < len)
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
