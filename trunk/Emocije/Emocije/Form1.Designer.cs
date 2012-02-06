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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.chartSubFeatures = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFeatShift = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFeatWindow = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSubWindowShift = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubWindowLength = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSampFreq = new System.Windows.Forms.TextBox();
            this.lblEmotion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartSubFeatures)).BeginInit();
            this.Settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 166);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chartSubFeatures
            // 
            legend1.Name = "Legend1";
            this.chartSubFeatures.Legends.Add(legend1);
            this.chartSubFeatures.Location = new System.Drawing.Point(403, 15);
            this.chartSubFeatures.Name = "chartSubFeatures";
            this.chartSubFeatures.Size = new System.Drawing.Size(524, 386);
            this.chartSubFeatures.TabIndex = 6;
            this.chartSubFeatures.Text = "chart1";
            // 
            // btnStart
            // 
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.Location = new System.Drawing.Point(38, 268);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(134, 133);
            this.btnStart.TabIndex = 10;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(178, 268);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(134, 133);
            this.btnStop.TabIndex = 11;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Settings
            // 
            this.Settings.Controls.Add(this.label5);
            this.Settings.Controls.Add(this.txtFeatShift);
            this.Settings.Controls.Add(this.label4);
            this.Settings.Controls.Add(this.txtFeatWindow);
            this.Settings.Controls.Add(this.button1);
            this.Settings.Controls.Add(this.label3);
            this.Settings.Controls.Add(this.txtSubWindowShift);
            this.Settings.Controls.Add(this.label2);
            this.Settings.Controls.Add(this.txtSubWindowLength);
            this.Settings.Controls.Add(this.label1);
            this.Settings.Controls.Add(this.txtSampFreq);
            this.Settings.Location = new System.Drawing.Point(12, 12);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(336, 203);
            this.Settings.TabIndex = 13;
            this.Settings.TabStop = false;
            this.Settings.Text = "Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Feature Window Shift (in samples):";
            // 
            // txtFeatShift
            // 
            this.txtFeatShift.Location = new System.Drawing.Point(200, 129);
            this.txtFeatShift.Name = "txtFeatShift";
            this.txtFeatShift.Size = new System.Drawing.Size(100, 20);
            this.txtFeatShift.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Feature Window Length (in samples):";
            // 
            // txtFeatWindow
            // 
            this.txtFeatWindow.Location = new System.Drawing.Point(200, 103);
            this.txtFeatWindow.Name = "txtFeatWindow";
            this.txtFeatWindow.Size = new System.Drawing.Size(100, 20);
            this.txtFeatWindow.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "SubWindow Shift (in samples):";
            // 
            // txtSubWindowShift
            // 
            this.txtSubWindowShift.Location = new System.Drawing.Point(200, 77);
            this.txtSubWindowShift.Name = "txtSubWindowShift";
            this.txtSubWindowShift.Size = new System.Drawing.Size(100, 20);
            this.txtSubWindowShift.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "SubWindow Length (in samples):";
            // 
            // txtSubWindowLength
            // 
            this.txtSubWindowLength.Location = new System.Drawing.Point(200, 51);
            this.txtSubWindowLength.Name = "txtSubWindowLength";
            this.txtSubWindowLength.Size = new System.Drawing.Size(100, 20);
            this.txtSubWindowLength.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sampling frequency:";
            // 
            // txtSampFreq
            // 
            this.txtSampFreq.Location = new System.Drawing.Point(200, 25);
            this.txtSampFreq.Name = "txtSampFreq";
            this.txtSampFreq.Size = new System.Drawing.Size(100, 20);
            this.txtSampFreq.TabIndex = 13;
            // 
            // lblEmotion
            // 
            this.lblEmotion.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblEmotion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblEmotion.Location = new System.Drawing.Point(12, 218);
            this.lblEmotion.Name = "lblEmotion";
            this.lblEmotion.Size = new System.Drawing.Size(336, 47);
            this.lblEmotion.TabIndex = 14;
            this.lblEmotion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 579);
            this.Controls.Add(this.lblEmotion);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.chartSubFeatures);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Emocije";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartSubFeatures)).EndInit();
            this.Settings.ResumeLayout(false);
            this.Settings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSubFeatures;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox Settings;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFeatShift;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFeatWindow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSubWindowShift;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSubWindowLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSampFreq;
        private System.Windows.Forms.Label lblEmotion;
    }
}

