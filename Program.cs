using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComPortTest
{
    class Program
    {
        // Create the serial port with basic settings
        private static SerialPort sendPort;       // Create the serial port with basic settings
        private static SerialPort readPort = new SerialPort("COM4",
          57600, Parity.None, 8, StopBits.One);

        [STAThread]
        static void Main(string[] args)
        {


            Console.WriteLine("Select the COM port first.");
            var portList = SerialPort.GetPortNames();
            for (int i = 0; i < portList.Length; i++)
            {
                Console.WriteLine($"{i}: {portList[i]}");
            }
            var portId = Console.ReadLine();
            var portName = portList[int.Parse(portId)];
            Console.WriteLine("Select port: " + portName);
            sendPort = new SerialPort(portName,
          57600, Parity.None, 8, StopBits.One);
            sendPort.WriteTimeout = 1000;


            // Instatiate this class
            new Thread(() => { sendingSignal(); }).Start();


            Console.ReadLine();
        }

        static void sendingSignal()
        {
            sendPort.Open();
            while (true)
            {
                try
                {
                    Console.WriteLine("Sending 11-111-UL");
                    sendPort.WriteLine("11-111-UL");
                }
                catch (Exception ex)
                {


                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }

        }


    }
}
