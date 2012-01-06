namespace Emocije
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.button1 = new System.Windows.Forms.Button();
            this.volume = new System.Windows.Forms.ProgressBar();
            this.prbClassificationResult = new System.Windows.Forms.ProgressBar();
            this.chartSubFeatures = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartSubFeatures)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // volume
            // 
            this.volume.Location = new System.Drawing.Point(32, 67);
            this.volume.Maximum = 256;
            this.volume.Name = "volume";
            this.volume.Size = new System.Drawing.Size(324, 23);
            this.volume.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.volume.TabIndex = 1;
            // 
            // prbClassificationResult
            // 
            this.prbClassificationResult.Location = new System.Drawing.Point(32, 215);
            this.prbClassificationResult.Maximum = 256;
            this.prbClassificationResult.Name = "prbClassificationResult";
            this.prbClassificationResult.Size = new System.Drawing.Size(185, 23);
            this.prbClassificationResult.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prbClassificationResult.TabIndex = 4;
            // 
            // chartSubFeatures
            // 
            legend1.Name = "Legend1";
            this.chartSubFeatures.Legends.Add(legend1);
            this.chartSubFeatures.Location = new System.Drawing.Point(403, 15);
            this.chartSubFeatures.Name = "chartSubFeatures";
            this.chartSubFeatures.Size = new System.Drawing.Size(524, 646);
            this.chartSubFeatures.TabIndex = 6;
            this.chartSubFeatures.Text = "chart1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(280, 259);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 682);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chartSubFeatures);
            this.Controls.Add(this.prbClassificationResult);
            this.Controls.Add(this.volume);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartSubFeatures)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar volume;
        private System.Windows.Forms.ProgressBar prbClassificationResult;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSubFeatures;
        private System.Windows.Forms.Button button2;
    }
}

