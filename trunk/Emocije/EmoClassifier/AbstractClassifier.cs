using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier
{
   

    public class SubFeaturesComputedEventArgs:EventArgs
    {
        public List<double> ComputedFeatures { get; set; }
    }

    /// <summary>
    /// Basic infrastructure for classifier
    /// </summary>
    public abstract class AbstractClassifier
    {
        public delegate void ClassifComplete(object sender, ClassifierEventArgs e);
        public delegate void SubFeaturesComp(object sender, SubFeaturesComputedEventArgs e);


        private FastQueue<double> SubData { get; set; }
        public List<IFeature> SubFeatures { get; protected set; } 
        public int SubWindowLength { get; set; }
        public int SubWindowShift { get; set; }
        private Dictionary<IFeature, FastQueue<double>> SubResults { get; set; }


        public Dictionary<IFeature,List<IFeature>> SuperFeatures { get; protected set; }        
        public int SuperWindowLength { get; set; }
        public int SuperWindowShift { get; set; }
        public List<double> FinalFeatures { get; protected set; }
        
        
        public List<EmoClassifierResult> Results { get; protected set; }

        public IDataProvider DataProvider { get; set; }

        /// <summary>
        /// Fires if classification is complete
        /// </summary>
        public abstract event ClassifComplete ClassificationComplete;
        
        /// <summary>
        /// Fires if SubFeatures are computed
        /// </summary>
        public event SubFeaturesComp SubFeaturesComputed;

        public AbstractClassifier()
        {
            Results = new List<EmoClassifierResult>();
            SubData = new FastQueue<double>(100000);
            SubFeatures = new List<IFeature>();
            SubResults = new Dictionary<IFeature,FastQueue<double>>();
            FinalFeatures = new List<double>();
            SuperFeatures = new Dictionary<IFeature,List<IFeature>>();


            this.SubWindowLength = 1102; // 44100 [samples per second] * 0.025 [25 milisecond interval]
            this.SubWindowShift = 661; // 44100 [samples per second] * 0.015 [15 milisecond interval]

            this.SuperWindowLength = 80; // 44100 [samples per second] / 1102 [SubFeatures per second] * 2 [seconds]
            this.SuperWindowShift = 40; // 44100 [samples per second] / 1102 [SubFeatures per second] * 1 [seconds]

            
            //Napravi feature
            MakeFeatures();

            foreach (IFeature f in SubFeatures)
            {
                SubResults.Add(f,new FastQueue<double>(10000));
            }

        }

        public string Description
        {
            get { return "This is a classifier"; }
            protected set{}
        }

        /// <summary>
        /// Adds input data as byte array to internal queue 
        /// </summary>
        /// <param name="Data"></param>
        public void EnqueueData(byte[] Data)
        {

            foreach (byte b in Data)
                this.SubData.Enqueue((double)b);
       
            if (this.SubData.Count > SubWindowLength)
                ComputeSubFeatures();
        }


        private void ComputeSubFeatures()
        {
            DateTime d = DateTime.Now;
            List<double> CurrentData;
    
            CurrentData = this.SubData.Peek(SubWindowLength);
            this.SubData.Delete(SubWindowShift);
            DataProvider.Data = CurrentData;

            List<double> ComputedFeatures = new List<double>();
            foreach (IFeature f in SubFeatures)
            {
                f.Compute();
                SubResults[f].Enqueue(f.Feature);
                ComputedFeatures.Add(f.Feature);
            }
            TimeSpan ts = DateTime.Now - d;
            int ms = ts.Milliseconds;
            SubFeaturesComputedEventArgs e = new SubFeaturesComputedEventArgs();
            e.ComputedFeatures = ComputedFeatures;
            SubFeaturesComputed.Invoke(this, e);

            if (SubResults.First().Value.Count > SuperWindowLength)
                ComputeSuperFeatures();

            if (this.SubData.Count > SubWindowLength)
                ComputeSubFeatures();
        }


        private void ComputeSuperFeatures()
        {
            foreach (IFeature f in SubFeatures)
            {
                FastQueue<double> CurrentQueue = SubResults[f];
                List<double> CurrentData = CurrentQueue.Peek(SuperWindowLength);
                CurrentQueue.Delete(SuperWindowShift);
                DataProvider.Data = CurrentData;
                
                List<IFeature> CurrentSuperFeatures = SuperFeatures[f];
                foreach (IFeature superf in CurrentSuperFeatures)
                {
                    superf.Compute();
                    FinalFeatures.Add(superf.Feature);
                }

            }
            Classify();
            FinalFeatures.Clear();
            if (SubResults.First().Value.Count > SuperWindowLength)
                ComputeSuperFeatures();
        }

        protected abstract void MakeFeatures();
        protected abstract void Classify();

    }
}
