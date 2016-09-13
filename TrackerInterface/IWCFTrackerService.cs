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
        //TODO Clean up un-needed service calls
        #region No longer needed
        [OperationContract]
        string getPlayerInfo(long playerID);
        [OperationContract]
        List<string> GetPlayers(string serverID);
        [OperationContract]
        long getSteamID(string name);
        #endregion
        //Client sends steam IDs,we pull data from the API based on the ID and store it.
        [OperationContract]
        void PullPlayers(string serverID);
        [OperationContract]
        //Returns a list of player object to the client
        List<Player> GetPlayerList(string serverID);
        [OperationContract]
        string getMySteamID(string steamName);
    }
}
