using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier.Classifiers
{
    public class GoodClassifier : AbstractClassifier
    {


        protected override void Classify()
        {
            // pročitaj podatke iz FinalFeatures
            // provedi klasifikaciju
            // rezultate spremi u res
            // ispali događaj ClassificationComplete

            EmoClassifierResult res = new EmoClassifierResult();
            
            
            ClassifierEventArgs e = new ClassifierEventArgs();
            e.Result = res;
            ClassificationComplete.Invoke(this, e);
            
        }

        protected override void MakeFeatures()
        {
            IDataProvider DataProvider = new DataProviders.FourierDataProvider();
            this.DataProvider = DataProvider;
            // dodaj podznačajke
            IFeature Sub_TimeEnergy = new Features.TimeEnergy(DataProvider);
            IFeature Sub_TimePowerDb = new Features.TimePowerDb(DataProvider);
            IFeature Sub_ZeroCrossingRate = new Features.ZeroCrossingRate(DataProvider);
            IFeature Sub_Pitch = new Features.Pitch(DataProvider);
            
            SubFeatures.Add(Sub_TimeEnergy);
            SubFeatures.Add(Sub_TimePowerDb);
            SubFeatures.Add(Sub_ZeroCrossingRate);
            SubFeatures.Add(Sub_Pitch);

            // dodaj nadznačajke
            IFeature Super_TimeAverage = new Features.TimeAverage(DataProvider);
            //IFeature Super_Maximum = new Features.Maximum(DataProvider);
 
            List<IFeature> SuperFeatList = new List<IFeature>();
            
            // napravi listu značajki za podznačajku Sub_TimeAverage
            SuperFeatList.Add(Super_TimeAverage);
            SuperFeatures.Add(Sub_TimeEnergy, SuperFeatList);

            // napravi listu značajki za podznačajku Sub_TimePowerDb
            SuperFeatList.Clear();
            SuperFeatList.Add(Super_TimeAverage);
            SuperFeatures.Add(Sub_TimePowerDb, SuperFeatList);

            SuperFeatList.Clear();
            // napravi listu značajki za podznačajku Sub_ZeroCrossingRate
            SuperFeatList.Add(Super_TimeAverage);
            SuperFeatures.Add(Sub_ZeroCrossingRate, SuperFeatList);

            SuperFeatList.Clear();
            SuperFeatList.Add(Super_TimeAverage);
            SuperFeatures.Add(Sub_Pitch, SuperFeatList);

        }

        public override event AbstractClassifier.ClassifComplete ClassificationComplete;
    }
}
