using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier
{
    public interface IDataProvider
    {
        List<double> Data { get; set; }
        List<double> PowerSpectra { get; set; }
        int SamplingFrequency { get; set; }
    }
}
