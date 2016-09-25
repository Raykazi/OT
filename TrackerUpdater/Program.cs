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
        static ChannelFactory<IWcfTrackerService> _channelFactory;
        static IWcfTrackerService _server;
        static Stopwatch _sw = new Stopwatch();
        static System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        static string[] _servers = new string[] { "arma_1", "arma_2_blame_poseidon", "arma_3" };
        private static readonly long RefreshTime = 60000;

        static void Main(string[] args)
        {
            Update();
            while (true)
            {
                if (!_sw.IsRunning) _sw.Start();
                switch (_sw.ElapsedMilliseconds)
                {
                    default:
                        if (_sw.IsRunning)
                        {
                            if (_sw.ElapsedMilliseconds >= RefreshTime)
                                Update();
                        }
                        break;
                }

            }
        }

        private static void timer_Tick(object sender, EventArgs e)
        {
        }

        private static void Update()
        {
            _servers.Select(id =>
            {
                Thread tr = new Thread(() => DoUpdate(id));
                tr.Start();
                return tr;

            }).ToList().ForEach(t => t.Join());
            _sw.Reset();
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
            Console.WriteLine(string.Format("[{0}] {1}", DateTime.Now, msg));
        }
    }
}
