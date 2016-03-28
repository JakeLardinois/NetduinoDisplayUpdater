using System;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

using NetduinoDisplayUpdater.Models;
using Rinsen.WebServer.FileAndDirectoryServer;


namespace NetduinoDisplayUpdater
{
    public class Program
    {
        private static MySerialPort COM1SerialPort;
        private static InterruptPort btnBoard { get; set; }
        private static OutputPort onboardLED = new OutputPort(Pins.ONBOARD_LED, false);
        public const string WORKINGDIRECTORY = @"\WWW";



        public static void Main()
        {

            // initialize the serial port for data being input via COM1
            COM1SerialPort = new MySerialPort(SerialPorts.COM1, BaudRate.Baudrate9600, Parity.None, DataBits.Eight, StopBits.One);

            //Setup the Onboard button; InterruptEdgeLevelLow only fires the event the first time that the button descends
            btnBoard = new InterruptPort(Pins.ONBOARD_SW1, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeHigh);
            // Create an event handler for the button
            btnBoard.OnInterrupt += new NativeEventHandler(btnBoard_OnInterrupt);


            var webServer = new WebServer();
            webServer.AddRequestFilter(new RequestFilter());
            var fileAndDirectoryService = new FileAndDirectoryService();
            fileAndDirectoryService.SetSDCardManager(new SDCardManager(WORKINGDIRECTORY));
            webServer.SetFileAndDirectoryService(fileAndDirectoryService);
            /*Setting a default controller removes the ability to browse the files and folder of the root web directory*/
            //webServer.RouteTable.DefaultControllerName = "Scale";
            webServer.StartServer(80);//If port is not specified, then default is port 8500
            webServer.SetHostName("Display Updater");
        }

        private static void btnBoard_OnInterrupt(uint port, uint data, DateTime time)
        {
            //For development purposes...
            #region 
            ////Makes the LED blink 3 times
            //BlinkOnboardLED(3, 300);
            ////Fires the Serial Port Data Recieved Event Listener to simulate data being recieved from the serial port. 
            //SerialDataReceivedEventArgs objSerialDataReceivedEventArgs = null;
            //IndicatorScannerSerialPort_DataReceived(new object(), objSerialDataReceivedEventArgs);
            #endregion

            COM1SerialPort.WriteString("Hello Serial Port!!");
        }

        private static void BlinkOnboardLED(int NoOfTimes, int WaitPeriod)
        {
            Debug.Print("Blinking LED");
            for (int intCounter = 2; intCounter < NoOfTimes * 2 + 1; intCounter++)
            {
                onboardLED.Write(intCounter % 2 == 1);
                Thread.Sleep(WaitPeriod);
            }
        }
    }
}
