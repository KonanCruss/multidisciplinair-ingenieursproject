using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultidisciplinairProject {
    public partial class MultidisciplinairProject : Form {
        Arduino arduino;
        Timer t;
        
        // Test
        public MultidisciplinairProject() {
            t = new Timer();

            InitializeComponent();
            Init();
        }

        public void Init() {
            mainChart.ChartAreas.Add("mainChart");
        }

        private void Start_Click(object sender, EventArgs e) {
            arduino = new Arduino(Port.Text);

            if(!arduino.IsOpen) {
                arduino.OpenPort();
            }

            switch(actionComboBox.SelectedText) {
                case "Go Up":
                    actionComboBoxGoUp_START();
                    break;
                case "Go Down":
                    actionComboBoxGoDown_START();
                    break;
                case "Measure":
                    actionComboBoxMeasure_START();
                    break;
                default:
                    MessageBox.Show("Error, undefined action has been selected", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void Stop_Click(object sender, EventArgs e) {
            if(arduino.IsOpen) {
                switch(actionComboBox.SelectedText) {
                    case "Go Up":
                        actionComboBoxGoUp_STOP();
                        break;
                    case "Go Down":
                        actionComboBoxGoDown_STOP();
                        break;
                    case "Measure":
                        actionComboBoxMeasure_STOP();
                        break;
                    default:
                        MessageBox.Show("Error, undefined action has been selected", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }

        }

        private void autoStop_CheckedChanged(object sender, EventArgs e) {
            Stop.Enabled = !autoStop.Checked;
        }

        private void t_TickMeasure(object sender, EventArgs e) {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Dispose(true);
        }

        private void SamplingRate_Validating(object sender, CancelEventArgs e) {
            if(Regex.IsMatch(SamplingRate.Text, "[^0-9]")) {
                MessageBox.Show("Only enter numbers!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void actionComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            switch(actionComboBox.SelectedIndex) {
                case 0:
                    autoStop.Checked = false;
                    autoStop.Enabled = false;
                    break;
                case 1:
                    autoStop.Checked = false;
                    autoStop.Enabled = false;
                    break;
                case 2:
                    autoStop.Enabled = true;
                    break;
                default:
                    MessageBox.Show("Unknown action selected", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        private void actionComboBoxGoUp_START() {

        }
        private void actionComboBoxGoDown_START() {

        }
        private void actionComboBoxMeasure_START() {
            int samplingRate = Convert.ToInt32(SamplingRate.Text);
            if(samplingRate < 1) {
                MessageBox.Show("Sampling rate must be higher then 0!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            t.Interval = samplingRate;
            try {
                arduino.ArduinoRead("12");
            } catch {
                MessageBox.Show("Couldn't send start signal to Arduino!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            t.Tick += new EventHandler(t_TickMeasure);
        }

        private void actionComboBoxGoDown_STOP() {

        }
        private void actionComboBoxGoUp_STOP() {

        }
        private void actionComboBoxMeasure_STOP() {

        }
    }
}