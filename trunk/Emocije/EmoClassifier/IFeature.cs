using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier
{
    public interface IFeature
    {
        string Description { get; }
        double Feature { get;   }
        void Compute();
        IDataProvider DataProvider { get; set; }
    }
}
