using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrackerInterface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServer
    {
        [OperationContract]
        void PlayerUpdate();
        [OperationContract]
        void HouseUpdate();
        [OperationContract]
        void GangWarsUpdate();

        [OperationContract]
        List<int> FetchWarTargets();

        [OperationContract]
        List<Player> FetchPlayers(int server);

        [OperationContract]
        void Login(string machineName, string userName);

        [OperationContract]
        void Logout(string machineName, string userName);
    }
}
