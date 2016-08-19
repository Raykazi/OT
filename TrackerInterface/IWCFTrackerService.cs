using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrackerInterface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFTrackerService" in both code and config file together.
    [ServiceContract]
    public interface IWCFTrackerService
    {
        [OperationContract]
        string getPlayerIfno(long playerID);
        [OperationContract]
        string getPlayers(string IP, int port);
        [OperationContract]
        long getSteamID(string name);
        [OperationContract]
        void updateDB(List<string> steamIDs);
        [OperationContract]
        List<Player> sendPlayers();
    }
}
