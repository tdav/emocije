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

            List<Features.ChangeRate> Sub_mfcc = new List<Features.ChangeRate>();
            
            SubFeatures.Add(Sub_TimeEnergy);
            SubFeatures.Add(Sub_TimePowerDb);
            SubFeatures.Add(Sub_ZeroCrossingRate);
            SubFeatures.Add(Sub_Pitch);

            // dodaj nadznačajke
            IFeature Super_TimeAverage = new Features.TimeAverage(DataProvider);
            IFeature Super_Maximum = new Features.Maximum(DataProvider);
            IFeature Super_Minimum = new Features.Minimum(DataProvider);
            IFeature Super_Range = new Features.Range(DataProvider);
            IFeature Super_Median = new Features.Median(DataProvider);
            IFeature Super_Std = new Features.Std(DataProvider);
            IFeature Super_Variance = new Features.Variance(DataProvider);
            IFeature Super_ChangeRate = new Features.ChangeRate(DataProvider);


            List<IFeature> SuperFeatList = new List<IFeature>();
                   
            SuperFeatList.Add(Super_TimeAverage);
            SuperFeatList.Add(Super_Maximum);
            SuperFeatList.Add(Super_Minimum);
            SuperFeatList.Add(Super_Range);
            SuperFeatList.Add(Super_Median);
            SuperFeatList.Add(Super_Std);
            SuperFeatList.Add(Super_Variance);
            SuperFeatList.Add(Super_ChangeRate);
                
            SuperFeatures.Add(Sub_TimeEnergy, SuperFeatList);
            SuperFeatures.Add(Sub_TimePowerDb, SuperFeatList);  
            SuperFeatures.Add(Sub_ZeroCrossingRate, SuperFeatList);
            SuperFeatures.Add(Sub_Pitch, SuperFeatList);

            SuperFeatList = new List<IFeature>();
            SuperFeatList.Add(Super_TimeAverage);

            for (int i = 0; i < 12; i++)
            {
                SuperFeatures.Add(new Features.ChangeRate(DataProvider),SuperFeatList);
            }

                
        }

        public override event AbstractClassifier.ClassifComplete ClassificationComplete;
    }
}
