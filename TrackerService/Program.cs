using System;
using System.ServiceModel;

namespace TrackerServer
{
    class Program
    {
        /// <summary>
        /// Log events to the console window, with a timestamp for when they occured
        /// </summary>
        public static void ConsoleLog(string msg)
        {
            Console.WriteLine($"[{DateTime.Now}] {msg}");
        }
        /// <summary>
        /// Opens the server and listens for connection
        /// </summary>
        /// <param name="args"></param>
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
        /*Event based handlers*/
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
