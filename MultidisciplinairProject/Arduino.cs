using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace MultidisciplinairProject {
    class Arduino {
        /// <summary>
        /// Serial port which is used to communicate with
        /// </summary>
        SerialPort arduino;

        /// <summary>
        /// IsOpen property, checks if the port has been opened for communication
        /// </summary>
        public bool IsOpen {
            get {
                return arduino.IsOpen;
            }
        }

        /// <summary>
        /// Constructor: opens the serialport, on baudrate 9600
        /// </summary> 
        public Arduino (string port) {
            arduino = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
        }
        
        /// <summary>
        /// Opens the Arduino port
        /// </summary>
        public void OpenPort() {
            arduino.Open();
        }
        /// <summary>
        /// Closes the Arduino port
        /// </summary>
        public void ClosePort() {
            arduino.Close();
        }
        /// <summary>
        /// Reads the port and returns the message
        /// </summary>
        /// <returns>string message</returns>
        public string ReadArduino() {
            try {
                return arduino.ReadLine();
            } catch {
                Console.WriteLine("Unable to find Arduino! Make sure it has been opened.");
                return null;
            }
        }
        /// <summary>
        /// Sends a message to the Arduino
        /// </summary>
        /// <param name="message">Message for the arduino</param>
        public void ArduinoRead(string message) {
            arduino.Write(message);
        }
    }
}