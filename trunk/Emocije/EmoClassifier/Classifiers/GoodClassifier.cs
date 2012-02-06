﻿using System;
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
            /*
            double[] p = new double [14];
            string[,] mappedClassNames = new string[5,1] {{"anger"},{"sadness"},{"neutral"},{"happiness"},{"fear"}};
            double[,] x = new double [11,1];
            for (int i=0;i<11;i++)
            {
                x[i,1]=FinalFeatures[i];
            }


            double[,] mu = new double [11,5] {{0.0000,0.0001,0.0004,0.0000,0.0011},{0.0000,0.0000, 0.0000,0.0000,0.0001},{0.0013, 0.0006, 0.0008, 0.0011,0.0005},
            {0.0003,0.0010,0.0004,0.0001,0.0021},{-0.0000,-0.0000,0.0000,-0.0000,0.0001},{0.0002, 0.0002,0.0002 ,0.0002 ,0.0001},
            { 0.0000,0.0000,0.0000,0.0000,0.0000},{ 0.0145,0.0285,0.0326,0.0064,0.0393},{ 1.3798,0.6362,3.2903,3.6128 ,0.2600},{ 0.0065,0.0119,0.0134,0.0030,0.0155},
            {0.0000,0.0000,0.0000,0.0000,0.0000}};

            double[, ,] sigma = { {{ 76.449905, 2.288436, -3.813529, 13.418779, 29.629162, -0.858317, 0.000443, -87.129828, -1535.117882, -53.268922, 0.003419 }, { 2.288436, 0.267097, -0.487026, 1.293760, 0.904129, -0.110081, -0.000008, -2.186024, -548.363562, -3.509738, -0.000323}, 
                                                       {-3.813529,-0.487026,3.946255,-1.940819,-1.966273,0.621280,0.000138,132.281655,2609.973457,56.931371,0.002681},{13.418779,1.293760,-1.940819,91.999872,-4.661378,-0.051100,0.000377,1573.398488,-13117.865545,544.829938,0.000513},
                                                       {29.629162,0.904129,-1.966273,-4.661378,26.298600,-0.299032,0.000083,-116.722365,1801.365687,-49.817176,0.002735},{-0.858317,-0.110081,0.621280,-0.051100,-0.299032,0.191415,0.000024,47.276770,859.161886,20.641072,0.000739},
                                                       {0.000443,-0.000008,0.000138,0.000377,0.000083,0.000024,0.000001,0.015772,0.156504,0.005043,0.000002},{-87.129828,-2.186024,132.281655,1573.398488,-116.722365,47.276770,0.015772,63878.990193,351469.616020,25355.724502,0.731664},
                                                       {-1535.117882,-548.363562,2609.973457,-13117.865545,1801.365687,859.161886,0.156504,351469.616020,19201887.051616,160146.710996,18.684937}
                                                        ,{-53.268922,-3.509738,56.931371,544.829938,-49.817176,20.641072,0.005043,25355.724502,160146.710996,10737.340132,0.290839},{0.003419,-0.000323,0.002681,0.000513,0.002735,0.000739,0.000002,0.731664,18.684937,0.290839,0.000049}},
                                                        
                                                        {{0.078347,0.000128,0.023609,0.044950,0.019726,0.003981,-0.000016,-0.748214,-496.298340,-0.580347,-0.000449},{0.000128,0.000176,-0.014377,0.005688,-0.000252,-0.003142,-0.000000,0.188282,-24.790024,0.070444,0.000007},
                                                        {0.023609,-0.014377,2.251607,-0.154134,-0.057573,0.495997,0.000014,5.385191,1474.482064,4.458324,-0.000755},{0.044950,0.005688,-0.154134,1.578401,-0.025248,-0.014847,0.000250,79.520318,-573.878571,32.781587,0.005449},
                                                        {0.019726,-0.000252,-0.057573,-0.025248,0.066123,-0.004770,0.000008,-2.586396,-209.817521,-1.040178,-0.000144}
                                                        ,{0.003981,-0.003142,0.495997,-0.014847,-0.004770,0.175495,0.000067,7.046629,897.948028,3.508023,0.000827},{-0.000016,-0.000000,0.000014,0.000250,0.000008,0.000067,0.000001,0.025228,1.940739,0.010253,0.000007},
                                                        {-0.748214,0.188282,5.385191,79.520318,-2.586396,7.046629,0.025228,6091.624212,293135.782915,2586.086137,0.610282},{-496.298340,-24.790024,1474.482064,-573.878571,-209.817521,897.948028,1.940739,293135.782915,138766621.498651,116345.975072,88.885578},
                                                        {-0.580347,0.070444,4.458324,32.781587,-1.040178,3.508023,0.010253,2586.086137,116345.975072,1155.940085,0.248445},{-0.000449,0.000007,-0.000755,0.005449,-0.000144,0.000827,0.000007,0.610282,88.885578,0.248445,0.000155}},
                                                        
                                                        {{10.390918,-0.058070,-0.197155,0.684840,3.805180,0.141330,-0.000453,-65.333715,-24306.473920,-24.143684,-0.014722},{-0.058070,0.008328,-0.152344,-0.000224,-0.028213,-0.028493,0.000046,2.434261,1372.943599,1.275106,0.001686},
                                                        {-0.197155,-0.152344,15.404044,1.358211,-0.032453,2.827502,-0.000729,335.544007,11400.038448,148.975296,-0.019418},{0.684840,-0.000224,1.358211,0.429027,0.231460,0.242256,0.000036,80.565999,4504.450761,36.762589,0.001899},
                                                        {3.805180,-0.028213,-0.032453,0.231460,1.482280,0.049683,-0.000247,-37.414120,-11558.325243,-14.009469,-0.007770},{0.141330,-0.028493,2.827502,0.242256,0.049683,0.581898,-0.000137,83.279667,5561.378486,35.951422,-0.002350},
                                                        {-0.000453,0.000046,-0.000729,0.000036,-0.000247,-0.000137,0.000001,0.015472,10.272517,0.006850,0.000013},{-65.333715,2.434261,335.544007,80.565999,-37.414120,83.279667,0.015472,58718.297519,6027638.839455,25375.002585,2.201791},
                                                        {-24306.473920,1372.943599,11400.038448,4504.450761,-11558.325243,5561.378486,10.272517,6027638.839455,1255727515.316939,2569755.040118,637.534147},{-24.143684,1.275106,148.975296,36.762589,-14.009469,35.951422,0.006850,25375.002585,2569755.040118,11109.106226,0.968688},
                                                        {-0.014722,0.001686,-0.019418,0.001899,-0.007770,-0.002350,0.000013,2.201791,637.534147,0.968688,0.000526}},

                                                        {{0.000039,0.000004,-0.009617,0.000282,-0.000029,-0.000705,0.000001,-0.007562,-29.755387,-0.004853,0.000008},{0.000004,0.000003,-0.002870,0.000166,-0.000002,-0.000461,0.000000,-0.001781,-11.925859,-0.000669,-0.000000},
                                                            {-0.009617,-0.002870,12.542898,-0.016305,-0.030009,0.931593,0.000087,40.150071,10107.423890,17.521410,0.001941},{0.000282,0.000166,-0.016305,0.075069,-0.000888,-0.043866,0.000067,2.854963,-3467.985553,1.386591,0.000592},
                                                            {-0.000029,-0.000002,-0.030009,-0.000888,0.003038,0.002350,-0.000005,-0.191908,10.412818,-0.075311,-0.000049},{-0.000705,-0.000461,0.931593,-0.043866,0.002350,0.261209,0.000061,2.804961,3901.185980,1.228100,0.000644},
                                                            {0.000001,0.000000,0.000087,0.000067,-0.000005,0.000061,0.000002,0.003624,-3.348998,0.001154,0.000005},{-0.007562,-0.001781,40.150071,2.854963,-0.191908,2.804961,0.003624,418.241606,-58592.224949,191.260149,0.052186},
                                                            {-29.755387,-11.925859,10107.423890,-3467.985553,10.412818,3901.185980,-3.348998,-58592.224949,678550932.194213,-45589.530575,2.838969},{-0.004853,-0.000669,17.521410,1.386591,-0.075311,1.228100,0.001154,191.260149,-45589.530575,94.571735,0.019662},
                                                            {0.000008,-0.000000,0.001941,0.000592,-0.000049,0.000644,0.000005,0.052186,2.838969,0.019662,0.000060}},

                                                            {{0.000039,0.000019,-0.016979,-0.000160,0.000262,-0.001650,-0.000001,-0.041291,117.404250,-0.013412,0.000009},{0.000019,0.000018,-0.011291,0.000836,0.000140,-0.001131,-0.000000,0.043054,27.468333,0.018847,0.000008},
                                                                {-0.016979,-0.011291,12.867040,-0.672341,-0.105977,1.208775,0.000388,57.164787,-32622.750042,21.867919,-0.002394},{-0.000160,0.000836,-0.672341,6.336894,-0.104844,-0.500486,0.000202,91.504996,-39010.596695,28.672878,-0.001917},
                                                                {0.000262,0.000140,-0.105977,-0.104844,0.192772,0.007881,0.000044,2.197675,2117.844356,0.914562,0.000696},{-0.001650,-0.001131,1.208775,-0.500486,0.007881,0.529825,0.000011,2.206683,3722.008739,3.968712,-0.000259},
                                                                {-0.000001,-0.000000,0.000388,0.000202,0.000044,0.000011,0.000001,0.019310,-3.321314,0.007492,0.000004},{-0.041291,0.043054,57.164787,91.504996,2.197675,2.206683,0.019310,11474.991571,-376694.903748,4886.639761,0.432751},
                                                                {117.404250,27.468333,-32622.750042,-39010.596695,2117.844356,3722.008739,-3.321314,-376694.903748,1603538453.297427,-88738.558231,182.277635},{-0.013412,0.018847,21.867919,28.672878,0.914562,3.968712,0.007492,4886.639761,-88738.558231,2377.423870,0.181292},
                                                                {0.000009,0.000008,-0.002394,-0.001917,0.000696,-0.000259,0.000004,0.432751,182.277635,0.181292,0.000100}}};
                                                                                                                                                                                                                                                                                            
            double[] weight = new double [5] {0.2707, 0.1727, 0.0909, 0.4108, 0.0550};
    
            
                         
        
            mu=Matrix.ScalarMultiply(Math.Pow(4,10),mu);
            double[] prob = new double[5];

            for (int j = 0; j < 5; j++)
            {
                //prob[j] = Matrix.Multiply(weight[j] * GaussDistrib.Probability(x, mu, sigma, j));
            }

            */

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
