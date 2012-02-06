using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using System.IO;
using System.Globalization;

namespace EmoClassifier.Testing
{
    public class AllFeaturesComp
    {
        static Classifiers.GoodClassifier Classifier;

        public static void ComputeAllFeatures(string wavpath, string resultpath, System.Windows.Forms.TextBox progress)
        {
            int aa=0;
            StreamWriter writer = new StreamWriter("d:\\features.txt");
            for (int i = 1; i <= 674; i++)
            {
                

                WaveStream readerStream = new WaveFileReader(@"D:\Z projekti\Emocije\code\sustav(pun)\wavs\" + i.ToString()+ ".wav");
                WaveFormat format = new WaveFormat(readerStream.WaveFormat.SampleRate, 8, 1);
                readerStream = new WaveFormatConversionStream(format, readerStream);

                int length = (int)readerStream.Length;
                byte[] buffer = new byte[length];
                readerStream.Read(buffer, 0, length);

                Classifier = new Classifiers.GoodClassifier();
                Classifier.SamplingFrequency = 11025;
                Classifier.SubWindowLength = 256; // 44100 [samples per second] * 0.025 [25 milisecond interval]
                Classifier.SubWindowShift = 165; // 44100 [samples per second] * 0.015 [15 milisecond interval]

                Classifier.SuperWindowLength =  (int)Math.Floor((double)length / (Classifier.SubWindowShift))-10; // 44100 [samples per second] / 1102 [SubFeatures per second] * 2 [seconds]
                Classifier.SuperWindowShift = 5; // 44100 [samples per second] / 1102 [SubFeatures per second] * 1 [seconds]

                Classifier.ClassificationComplete += new AbstractClassifier.ClassifComplete(Classifier_ClassificationComplete);
                Classifier.SubFeaturesComputed += new AbstractClassifier.SubFeaturesComp(Classifier_SubFeaturesComputed);
                Classifier.SuperFeaturesComputed += new AbstractClassifier.SuperFeaturesComp(Classifier_SuperFeaturesComputed);
                Classifier.EnqueueData(buffer);
               
                System.Windows.Forms.Application.DoEvents();
                string line = "";
                foreach (double d in Classifier.AllFeatures.First())
                {
                    line += d.ToString(CultureInfo.InvariantCulture) + ",";
                }
                line = line.TrimEnd(',');
                line += "\r\n";
                writer.WriteLine(line);
                writer.Flush();
                progress.Text = i.ToString();
            }
            writer.Flush();

        }

        static void Classifier_SubFeaturesComputed(object sender, SubFeaturesComputedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        static void Classifier_ClassificationComplete(object sender, ClassifierEventArgs e)
        {
            //throw new NotImplementedException();
        }


        static void Classifier_SuperFeaturesComputed(object sender, SubFeaturesComputedEventArgs e)
        {
           
            
        }
        
       
    }
}
