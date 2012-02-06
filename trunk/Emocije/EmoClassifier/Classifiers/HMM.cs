using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.IO;
using MiscUtil.Conversion;
using System.Diagnostics;
using System.Security;
using System.Threading;

namespace EmoClassifier.Classifiers
{
    public class HMM:AbstractClassifier
    {
        public override event AbstractClassifier.ClassifComplete ClassificationComplete;

        public override string Description
        {
            get
            {
                return "Hidden Markov Models";
            }
        }
        private int buffer=10;

        private List<List<double>> vectorList = new List<List<double>>();
        /*timeEnergyMax
                     timeEnergyMin
                     timeEnergyVar
                     timePowerDbRange
                     timePowerDbMedian
                     timePowerDbChangerate
                     timeZcrMin
                     timePitchMax
                     timePitchMin
                     frequencyEnergyMean
                     frequencyEnergyMin
                     frequencyPowerDbRange
                     frequencyZcrRange
                     frequencyPitchMean
                     frequencyPitchRange
                     frequencyPitchStd
                     frequencyPitchChangerate
                     mfcc1
                     mfcc2
                     mfcc9
                     mfcc10

         * */
        protected override void MakeFeatures()
        {

            IDataProvider DataProvider = new DataProviders.FourierDataProvider();
            this.DataProvider = DataProvider;


            //značajke
            IFeature Sub_TimeEnergy = new Features.TimeEnergy(DataProvider);
            IFeature Sub_TimePowerDb = new Features.TimePowerDb(DataProvider);
            IFeature Sub_ZeroCrossingRate = new Features.ZeroCrossingRate(DataProvider);
            IFeature Sub_Pitch = new Features.Pitch(DataProvider);

            SubFeatures.Add(Sub_TimeEnergy);
            SubFeatures.Add(Sub_TimePowerDb);
            SubFeatures.Add(Sub_ZeroCrossingRate);
            SubFeatures.Add(Sub_Pitch);

            IFeature Super_Median = new Features.Median(DataProvider);
            IFeature Super_Mean = new Features.TimeAverage(DataProvider);
            IFeature Super_Max = new Features.Maximum(DataProvider);
            IFeature Super_Min = new Features.Minimum(DataProvider);
            IFeature Super_Var = new Features.Variance(DataProvider);
            IFeature Super_Range = new Features.Range(DataProvider);
            IFeature Super_ChangeRate = new Features.ChangeRate(DataProvider);

            IFeature Super_FFTMean = new Features.FFTAverage(DataProvider);
            IFeature Super_FFTMedian = new Features.FFTMedian(DataProvider);
            IFeature Super_FFTMax = new Features.FFTMaximum(DataProvider);
            IFeature Super_FFTRange = new Features.FFTRange(DataProvider);
            IFeature Super_FFTStd = new Features.FFTStd(DataProvider);
            IFeature Super_FFTChangeRate = new Features.FFTChangeRate(DataProvider);
            IFeature Super_FFTMin = new Features.FFTMinimum(DataProvider);
            

            // Energy ->(time max, time min, time var, freq mean, freq min)
            List<IFeature> SuperFeatList = new List<IFeature>() { Super_Max, Super_Min, Super_Var, Super_FFTMean, Super_FFTMin };
            SuperFeatures.Add(Sub_TimeEnergy, SuperFeatList);

            // Power -> (time range, time median, time changerate, freq range)
            SuperFeatList = new List<IFeature>() {Super_Range, Super_Median, Super_ChangeRate, Super_FFTRange};
            SuperFeatures.Add(Sub_TimePowerDb, SuperFeatList);

            // Zcr -> (time min, freq range)
            SuperFeatList = new List<IFeature>() { Super_Min, Super_FFTRange };
            SuperFeatures.Add(Sub_ZeroCrossingRate, SuperFeatList);

            // Pitch -> (freq max, freq median, freq changerate)
            SuperFeatList = new List<IFeature>() {Super_Mean, Super_Max, Super_Min, Super_FFTMean,Super_FFTRange, Super_FFTStd, Super_FFTChangeRate };
            SuperFeatures.Add(Sub_Pitch, SuperFeatList);


            SuperFeatList = new List<IFeature>() { Super_Mean };
            IFeature mfcc1 = new Features.MFCC(DataProvider, (uint)(1));
            SuperFeatures.Add(mfcc1, SuperFeatList);
            IFeature mfcc2 = new Features.MFCC(DataProvider, (uint)(2));
            SuperFeatures.Add(mfcc2, SuperFeatList);
            IFeature mfcc9 = new Features.MFCC(DataProvider, (uint)(9));
            SuperFeatures.Add(mfcc9, SuperFeatList);
            IFeature mfcc10 = new Features.MFCC(DataProvider, (uint)(10));
            SuperFeatures.Add(mfcc10, SuperFeatList);

            SubFeatures.Add(mfcc1);
            SubFeatures.Add(mfcc2);
            SubFeatures.Add(mfcc9);
            SubFeatures.Add(mfcc10);
        }
        static void OpenHVITE(string f)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"HVite.exe";
            startInfo.Arguments = f;
            startInfo.Verb = "runas";
            startInfo.UserName = "administrator";
            var pass = new SecureString();
            pass.AppendChar('l');
            pass.AppendChar('u');
            pass.AppendChar('k');
            pass.AppendChar('4');
            pass.AppendChar('L');
            pass.AppendChar('u');
            pass.AppendChar('k');
            pass.AppendChar('a');
            startInfo.Password = pass;
            startInfo.UseShellExecute = false;
            Process.Start(startInfo);
        }
        protected override void Classify()
        {
            for (int i = 0; i < FinalFeatures.Count(); i++)
            {
                if (Double.IsNaN(FinalFeatures[i]))
                    FinalFeatures[i] = 0;
            }
            
            if (MakeHTK(FinalFeatures, "1.htk", 0.001f))
            {
                string f = "-n 5 5 -C hvite.conf -H macros -H models.mmf -S testFiles.txt -l * -i result.mlf -w wordnet transcript classList";
                
                OpenHVITE(f);
                
                string pobjednik = winner("result.mlf");
                EmoClassifierResult res = new EmoClassifierResult();

                res.Anger = -prob[2];
                res.Sadness = -prob[1];
                res.Neutral = -prob[0];
                res.Joy = -prob[4];
                res.Fear = -prob[3];


                ClassifierEventArgs e = new ClassifierEventArgs();
                e.Result = res;
                ClassificationComplete.Invoke(this, e);
            }
        }
        public bool MakeHTK(List<double> vector, string pathFile, float vechotime)
        {
            this.vectorList.Add(vector);

            if (this.vectorList.Count < this.buffer) return false;
            else
            {
                List<List<Double>> tempList = new List<List<double>>();
                tempList = this.LastN();
                saveHTK(tempList, vechotime, pathFile);
                return true;

            }

        }

        public List<List<Double>> LastN()
        {
            List<List<Double>> temp = new List<List<double>>();
            List<Double> piece = new List<double>();
            for (int i = 0; i < this.buffer; i++)
            {
                piece = this.vectorList.ElementAt(this.vectorList.Count - i - 1);
                temp.Add(piece);
            }
            return temp;
        }
        /*
        neutral
        sadness
        anger
        fear
        happiness
        */
        double[] prob = new double[5];
        public string winner(string pathFile)
        {
            double max = double.MinValue;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(pathFile);
            string[] list = new string[100];
            string win = "null";
            for (int i = 0; i < 13; i++)
            {
                line = file.ReadLine();
                list[i] = line;
            }
            int j=0;
            for (int i = 2; i < 12; i = i + 2)
            {
                string[] words = list[i].Split(' ');
                double value = double.Parse(words[3]);
                prob[j++] = value;
                if (value > max)
                {
                    max = value;
                    win = words[2];
                }

            }
            file.Close();
            return win;
        }
        public void saveHTK(List<List<double>> VEC, float vechoptime, string path)
        {
            if (vechoptime > 0.1)
                vechoptime = 0.1f;
            if (vechoptime < 0.0000001)
                vechoptime = 0.0000001f;

            int nSamples = VEC.Count();
            int nFeatures = VEC.First().Count();
            uint sampPeriod = (uint)Math.Floor(vechoptime * Math.Pow(10, 7));
            ushort sampSize = (ushort)(4 * nFeatures);
            ushort parmKind = 9;

            EndianBinaryWriter bw = new EndianBinaryWriter(EndianBitConverter.Big, System.IO.File.Open(path, System.IO.FileMode.Create));
            bw.Write((uint)nSamples);
            bw.Write(sampPeriod);
            bw.Write(sampSize);
            bw.Write(parmKind);

            for (int i = 0; i < nSamples; i++)
            {
                for (int j = 0; j < nFeatures; j++)
                {
                    bw.Write((float)VEC[i][j]);
                }
            }
            bw.Close();

        }
    }
}
