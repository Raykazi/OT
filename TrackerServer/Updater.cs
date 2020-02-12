using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackerInterface;

namespace TrackerServer
{
    class Updater
    {
        private readonly Stopwatch _timer;
        private const long PlayerRefreshTime = 60000;
        private const long HouseRefreshTime = 1800000;
        private const long GangRefreshTime = 1800000;
        private long _playerRunTime;
        private long _houseRunTime;
        private long _gangRunTime;

        private bool _firstRun;

        /*WCF Variables*/
        private ChannelFactory<IServer> _channelFactory;
        private IServer _server;

        public Updater()
        {
            _timer = new Stopwatch();
            _timer.Start();
            _firstRun = true;
        }
        /// <summary>
        /// Log events to the console window, with a timestamp for when they occured
        /// </summary>
        public void ConsoleLog(string msg)
        {
            Console.WriteLine($"[{DateTime.Now}] (UPDATER): {msg}");
        }
        /// <summary>
        /// Open WCF Connection
        /// </summary>
        private void OpenConnection()
        {
            _channelFactory = new ChannelFactory<IServer>("OTConnection");

            _server = _channelFactory.CreateChannel();
        }
        /// <summary>
        /// Close the WCF Connection
        /// </summary>
        private void CloseConnection()
        {
            if (_channelFactory.State < CommunicationState.Closing)
            {
                _channelFactory.Close();
            }
        }

        public void Run()
        {
            try
            {
                OpenConnection();
                while (true)
                {
                    if (_timer.ElapsedMilliseconds - _playerRunTime >= PlayerRefreshTime || _firstRun)
                    {
                        _server.PlayerUpdate();
                        _playerRunTime = _timer.ElapsedMilliseconds;
                    }

                    //if (_timer.ElapsedMilliseconds - _houseRunTime >= HouseRefreshTime || _firstRun)
                    //{
                    //    _server.HouseUpdate();
                    //    _houseRunTime = _timer.ElapsedMilliseconds;
                    //}
                    if (_timer.ElapsedMilliseconds - _gangRunTime >= GangRefreshTime || _firstRun)
                    {
                        _server.GangWarsUpdate();
                        _gangRunTime = _timer.ElapsedMilliseconds;
                    }

                    if (_firstRun) _firstRun = false;
                }
            }
            catch (Exception e)
            {
                ConsoleLog(e.Message);
                ConsoleLog(e.StackTrace);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
