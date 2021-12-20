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
          57600, Parity.None, 8, StopBits.One)
        {
            Encoding = Encoding.ASCII
        };

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
          57600, Parity.None, 8, StopBits.One)
            { Encoding = Encoding.ASCII };
            sendPort.WriteTimeout = 1000;


            // Instatiate this class
            new Thread(() => { sendingSignal(); }).Start();



            //readPort.Encoding = Encoding.ASCII;
            //readPort.Open();
            //readPort.DataReceived += (s, e) =>
            //{
            //    var buffer = new byte[1];
            //    var g = readPort.Read(buffer, 0, 1);

            //    Console.WriteLine("Read: " + buffer[0]);
            //};

            Console.ReadLine();
        }

        static void sendingSignal()
        {
            int count = 0;

            sendPort.Open();
            while (true)
            {
                try
                {

                    Console.WriteLine("Sending " + count);

                    //write 0 first
                    sendPort.Write(new byte[] { Byte.Parse("0") }, 0, 1);
                    byte[] buffer = new byte[] { Convert.ToByte(count) };
                    sendPort.Write(buffer, 0, 1);
                }
                catch (Exception ex)
                {


                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    count++;

                    Thread.Sleep(1000);
                }
            }

        }


    }
}
