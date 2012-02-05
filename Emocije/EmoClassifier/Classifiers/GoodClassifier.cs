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

            //double[] p = new double [14];
            //double[] mappedClassNames = new double [14];
            //double[] x = new double [14];

            //struct model{
            //    double[,] mu;
            //    double[,,] sigma;
            //    double[] weight;
            //}
       

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

            IFeature Super_FFTAverage = new Features.FFTAverage(DataProvider);
            IFeature Super_FFTMaximum = new Features.FFTMaximum(DataProvider);
            IFeature Super_FFTMinimum = new Features.FFTMinimum(DataProvider);
            IFeature Super_FFTRange = new Features.FFTRange(DataProvider);
            IFeature Super_FFTMedian = new Features.FFTMedian(DataProvider);
            IFeature Super_FFTStd = new Features.FFTStd(DataProvider);
            IFeature Super_FFTVariance = new Features.FFTVariance(DataProvider);
            IFeature Super_FFTChangeRate = new Features.FFTChangeRate(DataProvider);


            List<IFeature> SuperFeatList = new List<IFeature>();
                   
            SuperFeatList.Add(Super_TimeAverage);
            SuperFeatList.Add(Super_Maximum);
            SuperFeatList.Add(Super_Minimum);
            SuperFeatList.Add(Super_Range);
            SuperFeatList.Add(Super_Median);
            SuperFeatList.Add(Super_Std);
            SuperFeatList.Add(Super_Variance);
            SuperFeatList.Add(Super_ChangeRate);
            SuperFeatList.Add(Super_FFTAverage);
            SuperFeatList.Add(Super_FFTMaximum);
            SuperFeatList.Add(Super_FFTMinimum);
            SuperFeatList.Add(Super_FFTRange);
            SuperFeatList.Add(Super_FFTMedian);
            SuperFeatList.Add(Super_FFTStd);
            SuperFeatList.Add(Super_FFTVariance);
            SuperFeatList.Add(Super_FFTChangeRate);


            SuperFeatures.Add(Sub_TimeEnergy, SuperFeatList);
            SuperFeatures.Add(Sub_TimePowerDb, SuperFeatList);  
            SuperFeatures.Add(Sub_ZeroCrossingRate, SuperFeatList);
            SuperFeatures.Add(Sub_Pitch, SuperFeatList);

            SuperFeatList = new List<IFeature>();
            SuperFeatList.Add(Super_TimeAverage);

            for (int i = 0; i < 12; i++)
            {
                IFeature f = new Features.MFCC(DataProvider,(uint)(i+1));
                SubFeatures.Add(f);
                SuperFeatures.Add(f,SuperFeatList);

            }

                
        }

        public override event AbstractClassifier.ClassifComplete ClassificationComplete;
    }
}
