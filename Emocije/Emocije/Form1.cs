using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;

namespace Emocije
{
    public partial class Form1 : Form
    {
        WaveIn waveInStream;
        EmoClassifier.Classifiers.GoodClassifier Classifier;
        double ChartTime = 0;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            waveInStream = new WaveIn();
            waveInStream.WaveFormat = new WaveFormat(44100,1);

            waveInStream.DataAvailable += new EventHandler<WaveInEventArgs>(waveInStream_DataAvailable);
            waveInStream.StartRecording();


            Classifier = new EmoClassifier.Classifiers.GoodClassifier();

            Classifier.SubWindowLength = 1024; // 44100 [samples per second] * 0.025 [25 milisecond interval]
            Classifier.SubWindowShift = 512; // 44100 [samples per second] * 0.015 [15 milisecond interval]

            Classifier.SuperWindowLength = 80; // 44100 [samples per second] / 1102 [SubFeatures per second] * 2 [seconds]
            Classifier.SuperWindowShift = 40; // 44100 [samples per second] / 1102 [SubFeatures per second] * 1 [seconds]

            Classifier.SamplingFrequency = 44100;
            Classifier.ClassificationComplete +=new EmoClassifier.AbstractClassifier.ClassifComplete(Classifier_ClassificationComplete);
            SetUpChart();
            Classifier.SubFeaturesComputed += new EmoClassifier.AbstractClassifier.SubFeaturesComp(Classifier_SubFeaturesComputed);
        }

        void Classifier_SubFeaturesComputed(object sender, EmoClassifier.SubFeaturesComputedEventArgs e)
        {
            int i = 0;
            foreach (string s in ShowOnGraph)
            {
                chartSubFeatures.Series[s].Points.AddXY(ChartTime, e.ComputedFeatures[i++]);
            }
            ChartTime++;


            while (chartSubFeatures.Series[0].Points.Count > 500)
            {
                foreach (string s in ShowOnGraph)
                {
                    chartSubFeatures.Series[s].Points.RemoveAt(0);
                }
            }
            foreach (string s in ShowOnGraph)
            {
                chartSubFeatures.ChartAreas[s].AxisX.Minimum = ChartTime - 500;
                chartSubFeatures.ChartAreas[s].AxisX.Maximum = chartSubFeatures.ChartAreas[s].AxisX.Minimum + 500;
            }
        }

        void Classifier_ClassificationComplete(object sender, EmoClassifier.ClassifierEventArgs e)
        {
            prbClassificationResult.Value = (int)e.Result.Anger;
        }

        void waveInStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            volume.Value = (int)e.Buffer.Average(x=>(double)x);
            Classifier.EnqueueData(e.Buffer);
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            waveInStream.StopRecording();
            waveInStream.Dispose();
            Application.Exit();
        }

        

        public List<string> ShowOnGraph = new List<string> {   "MFCC" };
        public void SetUpChart()
        {

            foreach (string s in ShowOnGraph)
            {
                chartSubFeatures.ChartAreas.Add(s);
                chartSubFeatures.ChartAreas[s].AxisX.MajorGrid.LineColor = Color.LightBlue;
                chartSubFeatures.ChartAreas[s].AxisY.MajorGrid.LineColor = Color.LightBlue;
                chartSubFeatures.ChartAreas[s].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                chartSubFeatures.ChartAreas[s].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                chartSubFeatures.ChartAreas[s].AxisX.LineColor = Color.LightBlue;
                chartSubFeatures.ChartAreas[s].AxisY.LineColor = Color.LightBlue;

                chartSubFeatures.Series.Add(s);
                chartSubFeatures.Series[s].ChartArea = s;
                chartSubFeatures.Series[s].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            }


            /*
            chartSubFeatures.ChartAreas["Average"].AxisY2.LineColor = Color.LightBlue;
            chartSubFeatures.ChartAreas["Average"].AxisY2.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartSubFeatures.ChartAreas["Average"].AxisY2.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.None;
            chartSubFeatures.ChartAreas["Average"].AxisY2.LineWidth = 1;
            chartSubFeatures.ChartAreas["Average"].AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            */

            //chartSubFeatures.Series["Average"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmoClassifier.IDataProvider DataProvider = new  EmoClassifier.DataProviders.FourierDataProvider();
            
            EmoClassifier.Testing.TestResult tr =
            EmoClassifier.Testing.TestManager.ExecuteTest(
                @"D:\Z projekti\Emocije\code\testovi\input_1.txt",
                @"D:\Z projekti\Emocije\code\testovi\output_1_energy.txt",
                DataProvider,
                new EmoClassifier.Features.TimeEnergy(DataProvider),1102,661);

            EmoClassifier.Testing.TestResult tr2 =
            EmoClassifier.Testing.TestManager.ExecuteTest(
                @"D:\Z projekti\Emocije\code\testovi\input_1.txt",
                @"D:\Z projekti\Emocije\code\testovi\output_1_powerdb.txt",
                DataProvider,
                new EmoClassifier.Features.TimePowerDb(DataProvider), 1102, 661);

            string s = "";

        }

    }
}
