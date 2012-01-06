using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier
{
    public class ClassifierEventArgs:EventArgs
    {
        public EmoClassifierResult Result;
    }
}
