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
using System.Windows.Forms.DataVisualization.Charting;

namespace MultidisciplinairProject {
    public partial class MultidisciplinairProject : Form {
        Arduino arduino;                                    // Arduino which is being used
        Timer t;                                            // Timer to get results every tick (can be configured from the GUI)
        int _action;                                        // Ocupied action

        Series measureSerie;                                // Serie which contains the data of the measurement
        int elapsedTime = 0;                                // Time of the measurement
        List<DataPoint> measurements;                       // Measurements list
        
        // Constructor:
        public MultidisciplinairProject() {
            t = new Timer();

            InitializeComponent();
            Init();
        }

        // User initializing of stuff
        public void Init() {
            mainChart.ChartAreas.Add("mainChart");
            mainChart.ChartAreas[0].AxisX.Title = "Time (s)";
            mainChart.ChartAreas[0].AxisY.Title = "Force (N)";
        }

        // Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Dispose(true);
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
            if(t.Enabled)
                return;
            arduino = new Arduino(Port.Text);
            _action = actionComboBox.SelectedIndex;

            if(!arduino.IsOpen) {
                try {
                    arduino.OpenPort();
                } catch {
                    MessageBox.Show("Unable to open the Arduino: Check your port!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            switch(_action) {
                case 0:
                    actionComboBoxGoUp_START();
                    break;
                case 1:
                    actionComboBoxGoDown_START();
                    break;
                case 2:
                    actionComboBoxMeasure_START();
                    break;
                default:
                    MessageBox.Show("Error, undefined action has been selected!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            Start.Enabled = false;
        }

        // Clicking on the stop button
        private void Stop_Click(object sender, EventArgs e) {
            if(arduino.IsOpen) {
                arduino.ArduinoRead("0");
                arduino.ClosePort();
            }
            if(t.Enabled) 
                t.Stop();
            Start.Enabled = true;
        }

        private void autoStop_CheckedChanged(object sender, EventArgs e) {
            Stop.Enabled = !autoStop.Checked;
        }

        // Go Up Action when clicking on start
        private void actionComboBoxGoUp_START() {
            try {
                arduino.ArduinoRead("2");
            }  catch {
                MessageBox.Show("Couldn't send start signal to Arduino!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        // Go Down Action when clicking on start
        private void actionComboBoxGoDown_START() {
            try {
                arduino.ArduinoRead("3");
            } catch {
                MessageBox.Show("Couldn't send start signal to Arduino!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        // Measure Action when clicking on start
        private void actionComboBoxMeasure_START() {
            mainChart.Series.Remove(measureSerie);
            if(!mainChart.Series.Contains(measureSerie)) {
                measureSerie = null;
                measureSerie = new Series("Tensile Test");
                measureSerie.ChartType = SeriesChartType.Point;
                mainChart.Series.Add(measureSerie);
                measureSerie.YValuesPerPoint = 1;           // Making sure only 1value can be assigned to 1 X value
            }
            
            // Error handling:
            try {
                arduino.ArduinoRead("1");
            } catch {
                MessageBox.Show("Couldn't send start signal to Arduino!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Timer setup:
            elapsedTime = 0;
            t.Interval = 100;
            t.Tick += new EventHandler(t_TickMeasure);
            t.Start();
        }

        // Tick event when measuring
        private void t_TickMeasure(object sender, EventArgs e) {
            // Reading the measurement
            //double measurement = convertArduinoData(Convert.ToInt32(arduino.ReadArduino()));
            double measurement = Convert.ToDouble(arduino.ReadArduino());                           // Testing purposes
            elapsedTime += t.Interval;

            // Adding the measurement
            measureSerie.Points.AddXY(((double) elapsedTime)/1000, measurement);
            measurements = measureSerie.Points.ToList();

            // Updating the time label
            updateLabel(elapsedTime);

            // Autostop
            if(autoStop.Checked) {
                if(measurements[measurements.Count-1].YValues[0] < 5) {
                    Stop_Click(sender, e);
                }
            }
        }

        private void updateLabel(int timeMS) {
            // The miliseconds:
            int miliseconds = timeMS % 1000;
            string ms = "";
            if(miliseconds < 10) {
                ms = "00" + miliseconds;
            } else if(miliseconds < 100) {
                ms = "0" + miliseconds;
            } else {
                ms = miliseconds.ToString();
                ;
            }
            // The seconds:
            int seconds = (timeMS / 1000) % 60;
            string s = "";
            if(seconds < 10) {
                s = "0" + seconds;
            } else {
                s = seconds.ToString();
            }
            // The minutes:
            int minutes = (seconds / 60) % 60;
            string min = "";
            if(minutes < 10) {
                min = "0" + minutes;
            } else {
                min = minutes.ToString();
            }

            // The hour:
            int hour = minutes / 60;
            string h = "";
            if(hour < 10) {
                h = "00" + hour;
            } else if(hour < 100) {
                h = "0" + hour;
            } else {
                h = hour.ToString();
                ;
            }
            TimeElapsed.Text = h + ":" + min + ":" + s + "." + ms;
        }

        private double convertArduinoData(int arduinoReading) {
            return 0.000255272672224251 * arduinoReading - 0.0912923946701554;
        }

        private void exportChartToolStripMenuItem_Click(object sender, EventArgs e) {
            mainChart.SaveImage("Chart_Measurement.jpeg", ChartImageFormat.Jpeg);
            MessageBox.Show("Chart saved as \"Chart_Measurement.jpeg\"", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            if(!t.Enabled) {
                mainChart.Series.Remove(measureSerie);
            }
        }
    }
}