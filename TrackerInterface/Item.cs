using System.Runtime.Serialization;

namespace TrackerInterface
{
    /// <summary>
    /// Class that handles  player's virtual Items
    /// </summary>
    [DataContract]
    public class Item
    {
        //Name of the item on the player
        [DataMember]
        public string Name { get; set; }
        //Amount of item on the player
        [DataMember]
        public int Amount { get; set; }
    }
}
