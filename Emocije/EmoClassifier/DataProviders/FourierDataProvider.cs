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
         
        public List<double> FourierDataReal { get; set; }
        public List<double> FourierDataImag { get; set; }

        private void CalculateFourierTransform()
        { }

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
