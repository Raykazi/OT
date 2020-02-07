using System;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackerInterface;

namespace TrackerServer
{
    class Program
    {

        /// <summary>
        /// Log events to the console window, with a timestamp for when they occured
        /// </summary>
        public static void ConsoleLog(string msg)
        {
            Console.WriteLine($"[{DateTime.Now}] (SERVER) {msg}");
        }
        static void Main(string[] args)
        {
            Task serverTask = Task.Factory.StartNew(RunServer);
            Thread.Sleep(5000);
            Task updateTask = Task.Factory.StartNew(RunUpdater);
            Task.WaitAll(serverTask, updateTask);
            ConsoleLog("Server shutting down.");
            Console.ReadKey();
        }

        private static void RunUpdater()
        {
            ConsoleLog("Upater Thread Started");
            Updater updater = new Updater();
            updater.Run();
        }

        private static void RunServer()
        {
            ConsoleLog("Server Thread Started");
            using (ServiceHost host = new ServiceHost(typeof(Server)))
            {
                host.Opening += Host_Opening;
                host.Opened += Host_Opened;
                host.Closing += Host_Closing;
                host.Closed += Host_Closed;
                host.Faulted += Host_Faulted;
                host.UnknownMessageReceived += Host_UnknownMessageReceived;
                
                host.Open();
                Console.ReadLine();
            }
        }

        private static void Host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            ConsoleLog($"[ERROR] {e.Message}");
        }

        private static void Host_Faulted(object sender, EventArgs e)
        {
            //ConsoleLog($"[FAULT] {}");
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
