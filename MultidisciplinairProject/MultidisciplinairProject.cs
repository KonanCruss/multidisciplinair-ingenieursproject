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

        public MultidisciplinairProject() {
            arduino = new Arduino("COM1");
            t = new Timer();

            InitializeComponent();
            Init();
        }

        public void Init() {
            mainChart.ChartAreas.Add("mainChart");
        }

        private void Start_Click(object sender, EventArgs e) {
            if(Regex.IsMatch(textBox1.Text, "[^0-9]")) {
                MessageBox.Show("Only enter numbers as Sampling Rate!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!arduino.IsOpen) {
                arduino.OpenPort();
            }

            int samplingRate = Convert.ToInt32(textBox1.Text);
            if(samplingRate < 1) {
                MessageBox.Show("Sampling rate must be higher then 0!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            t.Interval = samplingRate;
            try {
                arduino.ArduinoRead("11111111");
            } catch {
                MessageBox.Show("Couldn't send start signal to Arduino!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            t.Tick += new EventHandler(t_Tick);
        }

        private void Stop_Click(object sender, EventArgs e) {

        }

        private void autoStop_CheckedChanged(object sender, EventArgs e) {
            Stop.Enabled = !autoStop.Checked;
        }

        private void textBox1_Validating(object sender, CancelEventArgs e) {
            if(Regex.IsMatch(textBox1.Text, "[^0-9]")) {
                MessageBox.Show("Only enter numbers!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void t_Tick(object sender, EventArgs e) {

        }
    }
}