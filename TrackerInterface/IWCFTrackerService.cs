using System.Collections.Generic;
using System.ServiceModel;

namespace TrackerInterface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFTrackerService" in both code and config file together.
    [ServiceContract]
    public interface IWcfTrackerService
    {
        //TODO Clean up un-needed service calls
        #region No longer needed
        [OperationContract]
        string GetPlayerInfo(long playerId);
        [OperationContract]
        List<string> GetPlayers(string serverId);
        [OperationContract]
        long GetSteamId(string name);
        #endregion
        //Client sends steam IDs,we pull data from the API based on the ID and store it.
        [OperationContract]
        void PullPlayers(string serverId);
        [OperationContract]
        //Returns a list of player object to the client
        List<Player> GetPlayerList(int serverId);
        [OperationContract]
        string GetMySteamId(string steamName);
    }
}
