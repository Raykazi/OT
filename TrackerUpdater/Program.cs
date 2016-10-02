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
        private static ChannelFactory<IWcfTrackerService> _channelFactory;
        private static IWcfTrackerService _server;
        private static readonly Stopwatch Sw = new Stopwatch();
        private static readonly string[] Servers = new string[] { "arma_1" };
        private const long RefreshTime = 60000;

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

        //Log events to the console window, with a timestamp for when they occured
        public static void ConsoleLog(string msg)
        {
            Console.WriteLine($"[{DateTime.Now}] {msg}");
        }
    }
}
