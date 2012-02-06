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
using EmoClassifier.Classifiers;
using EmoClassifier;

namespace Emocije
{
    public partial class Form1 : Form
    {
        WaveIn waveInStream;
        double ChartTime = 0;
        NAudio.Gui.WaveformPainter WavePainter = new NAudio.Gui.WaveformPainter();
        AbstractClassifier Classifier;
        List<AbstractClassifier> ComboList = new List<AbstractClassifier>();

        public Form1()
        {
            InitializeComponent();


            WavePainter.Top = 320;
            WavePainter.Left = 20;
            WavePainter.Width = 900;
            WavePainter.Height = 370;
            WavePainter.Visible = true;
            WavePainter.ForeColor = Color.DarkBlue;
            this.Controls.Add(WavePainter);

            ComboList.Add(new GMM());
            ComboList.Add(new HMM());
            cbxClassifier.DataSource = ComboList;
            cbxClassifier.DisplayMember = "Description";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            
       
            txtSampFreq.Text = "44100";
            txtFeatWindow.Text = "80";
            txtFeatShift.Text = "40";
            txtSubWindowLength.Text = "1024";
            txtSubWindowShift.Text = "512";

            btnStart.Enabled = true;
            btnStop.Enabled = false;

            
           SetUpChart();
        }

        void Classifier_ClassificationComplete(object sender, EmoClassifier.ClassifierEventArgs e)
        {

            
            chartSubFeatures.Series["Anger"].Points.AddXY(ChartTime, e.Result.Anger);
            chartSubFeatures.Series["Fear"].Points.AddXY(ChartTime, e.Result.Fear);
            chartSubFeatures.Series["Joy"].Points.AddXY(ChartTime, e.Result.Joy);
            chartSubFeatures.Series["Neutral"].Points.AddXY(ChartTime, e.Result.Neutral);
            chartSubFeatures.Series["Sadness"].Points.AddXY(ChartTime, e.Result.Sadness);
            

            ChartTime+=20;


            while (chartSubFeatures.Series[0].Points.Count > 500)
            {

                    chartSubFeatures.Series["Chart"].Points.RemoveAt(0);
            }
            //foreach (string s in ShowOnGraph)
            {
                
                chartSubFeatures.ChartAreas["Chart"].AxisX.Minimum = ChartTime - 500;
                chartSubFeatures.ChartAreas["Chart"].AxisX.Maximum = chartSubFeatures.ChartAreas["Chart"].AxisX.Minimum + 500;
            }

            SetLabel(e.Result); 
        }

        void SetLabel(EmoClassifier.EmoClassifierResult res)
        {
            if (res.Anger > res.Fear && res.Anger > res.Joy && res.Anger > res.Neutral && res.Anger > res.Sadness)
                lblEmotion.Text = "ANGER";
            if (res.Fear > res.Anger && res.Fear > res.Joy && res.Fear > res.Neutral && res.Fear > res.Sadness)
                lblEmotion.Text = "FEAR";
            if (res.Joy > res.Anger && res.Joy > res.Fear && res.Joy > res.Neutral && res.Joy > res.Sadness)
                lblEmotion.Text = "JOY";
            if (res.Neutral > res.Anger && res.Neutral > res.Fear && res.Neutral > res.Joy && res.Neutral > res.Sadness)
                lblEmotion.Text = "NEUTRAL";
            if (res.Sadness > res.Anger && res.Sadness > res.Fear && res.Sadness > res.Neutral && res.Sadness > res.Joy)
                lblEmotion.Text = "SADNESS";
        }

        void waveInStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            //volume.Value = (int)e.Buffer.Average(x=>(double)x);
            Classifier.EnqueueData(e.Buffer);
            for (int i = 0; i < e.BytesRecorded; i++)
            {

                WavePainter.AddMax(((float)e.Buffer[i])/255.0f -0.5f);
            }
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                waveInStream.StopRecording();
                waveInStream.Dispose();
            }
            catch { }
            Application.Exit();
        }

        

        public List<string> ShowOnGraph = new List<string> {   "Anger", "Fear", "Joy" , "Neutral", "Sadness" };
        public void SetUpChart()
        {

            
            string s = "Chart";
                chartSubFeatures.ChartAreas.Add(s);
                chartSubFeatures.ChartAreas[s].AxisX.MajorGrid.LineColor = Color.LightBlue;
                chartSubFeatures.ChartAreas[s].AxisY.MajorGrid.LineColor = Color.LightBlue;
                chartSubFeatures.ChartAreas[s].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                chartSubFeatures.ChartAreas[s].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                chartSubFeatures.ChartAreas[s].AxisX.LineColor = Color.LightBlue;
                chartSubFeatures.ChartAreas[s].AxisY.LineColor = Color.LightBlue;
             foreach (string ss in ShowOnGraph)
                {
                chartSubFeatures.Series.Add(ss);
                chartSubFeatures.Series[ss].ChartArea = s;
                chartSubFeatures.Series[ss].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100;

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

        

        private void button3_Click(object sender, EventArgs e)
        {
            //EmoClassifier.Testing.AllFeaturesComp.ComputeAllFeatures("", "",textBox1);
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            int SamplingFreq;
            int SubWindowLength;
            int SubWindowShift;
            int SuperWindowLength;
            int SuperWindowShift;

            if (!int.TryParse(txtSampFreq.Text, out SamplingFreq))
                return;
            if (!int.TryParse(txtSubWindowLength.Text, out SubWindowLength))
                return;
            if (!int.TryParse(txtSubWindowShift.Text, out SubWindowShift))
                return;
            if (!int.TryParse(txtFeatWindow.Text, out SuperWindowLength))
                return;
            if (!int.TryParse(txtFeatShift.Text, out SuperWindowShift))
                return;


            Classifier = (AbstractClassifier)cbxClassifier.SelectedItem;
            Classifier.ClassificationComplete +=new EmoClassifier.AbstractClassifier.ClassifComplete(Classifier_ClassificationComplete);
  
            Classifier.SubWindowLength = SubWindowLength;
            Classifier.SubWindowShift = SubWindowShift;
            Classifier.SuperWindowLength = SuperWindowLength;
            Classifier.SuperWindowShift = SuperWindowShift;
            Classifier.SamplingFrequency = SamplingFreq;


            waveInStream = new WaveIn();
            waveInStream.WaveFormat = new WaveFormat(SamplingFreq, 8, 1);
            waveInStream.DataAvailable += new EventHandler<WaveInEventArgs>(waveInStream_DataAvailable);
            
            waveInStream.StartRecording();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            waveInStream.StopRecording();
            waveInStream.Dispose();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmoClassifier.Matrix x = new EmoClassifier.Matrix(2,1);
            x[0,0] = 3;
            x[1,0] = 2;
            EmoClassifier.Matrix mu = new EmoClassifier.Matrix(2,1);
            mu[0,0] = 1;
            mu[1,0] = 4;
            EmoClassifier.Matrix sigma = new EmoClassifier.Matrix(2,2);
            sigma[0,0] = 1;
            sigma[1,0] = 4;
            sigma[0,1] = 1;
            sigma[1,1] = 2;

            double res = EmoClassifier.GaussDistrib.Probability(x, mu, sigma);

            EmoClassifier.Matrix mat = new EmoClassifier.Matrix(EmoClassifier.Classifiers.GMM.sigma0);
        }

    }
}
