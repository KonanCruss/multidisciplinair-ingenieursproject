using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace MultidisciplinairProject {
    class Arduino {
        SerialPort arduino;

        public bool IsOpen {
            get {
                return arduino.IsOpen;
            }
        }


        public Arduino (string port) {
            arduino = new SerialPort(port, 19200, Parity.None, 8, StopBits.One);
        }

        public void OpenPort() {
            try {
                arduino.Open();
                Console.WriteLine("Check");
            } catch {
                Console.WriteLine("Unable to find Arduino! Check the port.");
            }
        }
        public void ClosePort() {
            arduino.Close();
        }
        public string ReadArduino() {
            try {
                return arduino.ReadLine();
            } catch {
                Console.WriteLine("Unable to find Arduino! Make sure it has been opened.");
                return null;
            }
        }
        public void ArduinoRead(string message) {
            arduino.Write(message);
        }
    }
}
