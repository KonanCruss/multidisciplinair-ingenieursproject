namespace MultidisciplinairProject
{
    partial class MultidisciplinairProject
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
            if(disposing && arduino.IsOpen) {
                arduino.ClosePort();
                arduino = null;
            }
            if(disposing && t.Enabled) {
                t = null;
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
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.autoStop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(12, 12);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(93, 12);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // autoStop
            // 
            this.autoStop.AutoSize = true;
            this.autoStop.Location = new System.Drawing.Point(174, 16);
            this.autoStop.Name = "autoStop";
            this.autoStop.Size = new System.Drawing.Size(98, 17);
            this.autoStop.TabIndex = 2;
            this.autoStop.Text = "Automatic Stop";
            this.autoStop.UseVisualStyleBackColor = true;
            this.autoStop.CheckedChanged += new System.EventHandler(this.autoStop_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(297, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "00:00:00.000";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(420, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "Sampling rate in ms";
            this.textBox1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // mainChart
            // 
            this.mainChart.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mainChart.Location = new System.Drawing.Point(12, 41);
            this.mainChart.Name = "mainChart";
            this.mainChart.Size = new System.Drawing.Size(711, 564);
            this.mainChart.TabIndex = 5;
            this.mainChart.Text = "mainChart";
            // 
            // MultidisciplinairProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 617);
            this.Controls.Add(this.mainChart);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.autoStop);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.MaximumSize = new System.Drawing.Size(751, 656);
            this.MinimumSize = new System.Drawing.Size(751, 656);
            this.Name = "MultidisciplinairProject";
            this.Text = "Multidisciplinair Project - Trekbank";
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.CheckBox autoStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
    }
}

