using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TrackerService
{
    class Program
    {
        //Log events to the console window, with a timestamp for when they occured
        public static void ConsoleLog(string msg)
        {
            Console.WriteLine(string.Format("[{0}] {1}", DateTime.Now, msg));
        }
        static void Main(string[] args)
        {
            //Host the server
            using (ServiceHost host = new ServiceHost(typeof(WCFTrackerService)))
            {
                host.Open();
                host.Opened += Host_Opened;
                Program.ConsoleLog("Server is open");
                Console.ReadLine();
            }
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            ConsoleLog("Connection Opened");
        }
    }
}
