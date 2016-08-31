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
        string getPlayerIfno(long playerID);
        [OperationContract]
        string getPlayers(string IP, int port);
        [OperationContract]
        long getSteamID(string name);
        #endregion
        //Client sends steam IDs,we pull data from the API based on the ID and store it.
        [OperationContract]
        void updateDB(List<string> steamIDs);
        [OperationContract]
        //Returns a list of player object to the client
        List<Player> sendPlayers();
        [OperationContract]
        string getMySteamID(string steamName);
    }
}
