﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TrackerService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WCFTrackerService)))
            {
                host.Open();
                Console.WriteLine("Server is open");
                Console.ReadLine();
            }
        }
    }
}
