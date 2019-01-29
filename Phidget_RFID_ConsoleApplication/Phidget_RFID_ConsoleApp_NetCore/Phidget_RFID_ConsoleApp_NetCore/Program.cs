using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Phidget22;
using Phidget22.Events;

namespace ConsoleApp1
{
    //Nuget needed: Install-Package Phidget22.NET -Version 1.0.0.20190107 
    class Program
    {
        private static RFID rfid = new RFID();
        static void Main(string[] args)
        {
            Console.WriteLine("Loading handlers");
            Console.WriteLine($"Tag: No tag detected");
            Console.WriteLine($"Device Status: Not Connected");

            rfid.Attach += new AttachEventHandler(rfid_Attach);

            rfid.Detach += new DetachEventHandler(rfid_Detach);
            rfid.Error += new ErrorEventHandler(rfid_Error);

            rfid.Tag += new RFIDTagEventHandler(rfid_Tag);
            rfid.TagLost += new RFIDTagLostEventHandler(rfid_TagLost);
            rfid.Open();            
            Console.WriteLine("Started all handlers");
            Console.ReadLine();
        }

        private static void rfid_Tag(object sender, RFIDTagEventArgs e)
        {
            string tag = "";
            if (e.Tag != tag)
            {
                tag = e.Tag;
                Console.WriteLine($"Tag: {e.Tag.ToString()}");                
            }
            else
            {
                Thread.Sleep(2000);
                tag = "";
            }
        }

        private static void rfid_TagLost(object sender, RFIDTagLostEventArgs e)
        {
            Console.WriteLine($"Done with tag: {e.Tag}");
        }

        private static void rfid_Attach(object sender, AttachEventArgs e)
        {
            Console.WriteLine($"Device Status: Connected");
            Console.WriteLine($"Serial Number: {rfid.DeviceSerialNumber.ToString()}");
            Console.WriteLine($"Version: {rfid.DeviceVersion}");
            rfid.AntennaEnabled = true;
        }

        private static void rfid_Detach(object sender, DetachEventArgs e)
        {
            Console.WriteLine($"Device Status: Not Connected");
        }

        private static void rfid_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"{e.Description}");
            Console.WriteLine($"press any key to quite");
            Console.ReadKey();
        }
     }
}
