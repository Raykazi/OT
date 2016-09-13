using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TrackerInterface;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace TrackerUpdater
{
    class Program
    {
        static ChannelFactory<IWCFTrackerService> channelFactory;
        static IWCFTrackerService server;
        static Stopwatch sw = new Stopwatch();
        static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        static string[] servers = new string[] { "arma_1",  "arma_3" };
        private static readonly long refreshTime = 60000;

        static void Main(string[] args)
        {
            Update();
            while (true)
            {
                if (!sw.IsRunning) sw.Start();
                switch (sw.ElapsedMilliseconds)
                {
                    default:
                        if (sw.IsRunning)
                        {
                            if (sw.ElapsedMilliseconds >= refreshTime)
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
            servers.Select(id =>
            {
                Thread tr = new Thread(() => DoUpdate(id));
                tr.Start();
                return tr;

            }).ToList().ForEach(t => t.Join());
            sw.Reset();
        }

        private static void DoUpdate(string serverID)
        {
            try
            {
                channelFactory = new ChannelFactory<IWCFTrackerService>("TrackerClientEndpoint");
                server = channelFactory.CreateChannel();
                server.PullPlayers(serverID);
                if (channelFactory.State < CommunicationState.Closing)
                    channelFactory.Close();
            }
            catch(Exception e)
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
