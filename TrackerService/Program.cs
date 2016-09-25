using System;
using System.ServiceModel;

namespace TrackerServer
{
    class Program
    {
        //Log events to the console window, with a timestamp for when they occured
        public static void ConsoleLog(string msg)
        {
            Console.WriteLine($"[{DateTime.Now}] {msg}");
        }
        static void Main(string[] args)
        {
            try
            {
                //Host the server
                using (ServiceHost host = new ServiceHost(typeof(WcfTrackerService)))
                {
                    host.Opening += Host_Opening;
                    host.Opened += Host_Opened;
                    host.Closing += Host_Closing;
                    host.Closed += Host_Closed;
                    host.Open();
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                ConsoleLog("[EXCEPTION] " + ex.Message);
            }
        }

        private static void Host_Closed(object sender, EventArgs e)
        {
            ConsoleLog("Server Offline");
        }

        private static void Host_Closing(object sender, EventArgs e)
        {
            ConsoleLog("Server Closing...");
        }

        private static void Host_Opening(object sender, EventArgs e)
        {
            ConsoleLog("Server Starting...");
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            ConsoleLog("Server Online");
        }
    }
}
