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
        Arduino arduino;                                    // Arduino which is being used
        Timer t;                                            // Timer to get results every tick (can be configured from the GUI)
        string _action;                                     // Ocupied action
        
        // Constructor:
        public MultidisciplinairProject() {
            t = new Timer();

            InitializeComponent();
            Init();
        }

        // User initializing of stuff
        public void Init() {
            mainChart.ChartAreas.Add("mainChart");
        }

        // Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Dispose(true);
        }

        // Control that there are only numbers in the samplingrate textbox
        private void SamplingRate_Validating(object sender, CancelEventArgs e) {
            if(Regex.IsMatch(SamplingRate.Text, "[^0-9]")) {
                MessageBox.Show("Only enter numbers!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        
        // GUI control foreach available action
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

        // Clicking on the start button
        private void Start_Click(object sender, EventArgs e) {
            arduino = new Arduino(Port.Text);
            _action = actionComboBox.SelectedText;

            if(!arduino.IsOpen) {
                arduino.OpenPort();
            }

            switch(_action) {
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
                    MessageBox.Show("Error, undefined action has been selected!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        // Clicking on the stop button
        private void Stop_Click(object sender, EventArgs e) {
            if(arduino.IsOpen)
                arduino.ClosePort();
            if(t.Enabled) 
                t.Stop();
        }

        private void autoStop_CheckedChanged(object sender, EventArgs e) {
            Stop.Enabled = !autoStop.Checked;
        }

        // Tick event when measuring
        private void t_TickMeasure(object sender, EventArgs e) {

        }

        // Go Up Action when clicking on start
        private void actionComboBoxGoUp_START() {

        }
        // Go Down Action when clicking on start
        private void actionComboBoxGoDown_START() {

        }
        // Measure Action when clicking on start
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
            t.Start();
        }
    }
}