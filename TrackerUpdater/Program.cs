using System;
using System.Linq;
using System.ServiceModel;
using TrackerInterface;
using System.Diagnostics;
using System.Threading;

namespace TrackerUpdater
{
    class Program
    {
        /* WCF Stuff */
        private static ChannelFactory<IWcfTrackerService> _channelFactory; 
        private static IWcfTrackerService _server;
        /*Loop Variables*/
        private static readonly Stopwatch Sw = new Stopwatch();        
        private const long RefreshTime = 60000;
        // Servers to pull from
        private static readonly string[] Servers = new string[] { "arma_1" };

        /// <summary>
        /// Infinite loop that calls the update method every minute
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Update();
            while (true)
            {
                if (!Sw.IsRunning) Sw.Start();
                switch (Sw.ElapsedMilliseconds)
                {
                    default:
                        if (Sw.IsRunning)
                        {
                            if (Sw.ElapsedMilliseconds >= RefreshTime)
                                Update();
                        }
                        break;
                }

            }
        }
        /// <summary>
        /// Starts a new thread per server that is defined
        /// </summary>
        private static void Update()
        {
            Servers.Select(id =>
            {
                Thread tr = new Thread(() => DoUpdate(id));
                tr.Start();
                return tr;

            }).ToList().ForEach(t => t.Join());
            Sw.Reset();
        }
        /// <summary>
        /// Calls the method to pull player info from the server
        /// </summary>
        /// <param name="serverId"></param>
        private static void DoUpdate(string serverId)
        {
            try
            {
                _channelFactory = new ChannelFactory<IWcfTrackerService>("TrackerClientEndpoint");
                _server = _channelFactory.CreateChannel();
                _server.PullPlayers(serverId);
                if (_channelFactory.State < CommunicationState.Closing)
                    _channelFactory.Close();
            }
            catch (Exception e)
            {
                ConsoleLog(e.Message);
            }
        }
        /// <summary>
        /// Log events to the console window, with a timestamp for when they occured
        /// </summary>
        /// <param name="msg"></param>
        public static void ConsoleLog(string msg)
        {
            Console.WriteLine($"[{DateTime.Now}] {msg}");
        }
    }
}
