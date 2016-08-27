using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace TrackerInterface
{
    //Class for the virtual items
    [DataContract]
    public class VirtualItem
    {
        //Name of the item on the player
        [DataMember]
        public string name { get; set; }
        //Amount of item on the player
        [DataMember]
        public int amount { get; set; }

    }
    //Class for vehicles owned by the player.
    [DataContract]
    public class Vehicles
    {
        //Olympus vehicle ID
        [DataMember]
        public int ID { get; private set; }
        //Vehicle name
        [DataMember]
        public string name { get; private set; }
        //
        [DataMember]
        public int alive { get; private set; }
        [DataMember]
        //If the vehicle is out on the map right now
        public int active { get; private set; }
        [DataMember]
        //1 Basic Insurance 2 Full Insurance
        public int insuranceLevel { get; private set; }
        [DataMember]
        //Tier 1-4, speed and manueverability level
        public int turboLevel { get; private set; }
        [DataMember]
        //1 or 2Security level for the car
        public int secLevel { get; private set; }
        [DataMember]
        //1-4 Space of the vehicle
        public int storageLevel { get; private set; }
        public Vehicles(int ID, string name, int alive, int active, int iLvl, int tLvl, int secLvl, int stoLvl)
        {
            this.ID = ID;
            this.name = name;
            this.alive = alive;
            this.active = active;
            insuranceLevel = iLvl;
            turboLevel = tLvl;
            secLevel = secLvl;
            storageLevel = stoLvl;
        }
    }
    //Class for the player
    [DataContract]
    public class Player
    {
        [DataMember]
        //Olympus User ID
        public int UID { get; private set; }
        [DataMember]
        //Player Steam64 ID
        public long steamID { get; private set; }
        [DataMember]
        //Player Name
        public string name { get; private set; }
        [DataMember]
        //Cash on hand
        public int cash { get; private set; }
        [DataMember]
        //bank amount
        public int bank { get; private set; }
        [DataMember]
        //APD Cop Rank
        public int copLevel { get; private set; }
        [DataMember]
        //R&R Rank
        public int medicLevel { get; private set; }
        [DataMember]
        //Sneaky Admin rank
        public int adminLevel { get; private set; }
        [DataMember]
        //Donator dollar amount
        public int donatorLevel { get; private set; }
        [DataMember]
        //Aliases the player has gone by, tied to either their steamID or GUID
        public List<string> aliases { get; private set; }
        [DataMember]
        //Player Kills
        public int kills { get; private set; }
        [DataMember]
        //Player Deaths
        public int deaths { get; private set; }
        [DataMember]
        //R&R Revives and maybe Epi pens
        public int medicRevives { get; private set; }
        [DataMember]
        //Amount they have received APD Ticketing and maybe vigi-ing
        public int bountyCollected { get; private set; }
        [DataMember]
        //APD Arrest and maybe Vigi Arrest
        public int copArrest { get; private set; }
        [DataMember]
        //Civilian time
        public int timeCiv { get; private set; }
        [DataMember]
        //APD Time
        public int timeApd { get; private set; }
        [DataMember]
        //R&R Time
        public int timeMed { get; private set; }
        [DataMember]
        //How much the player is worth
        public int bountyWanted { get; private set; }
        [DataMember]
        //Gang that the player is in, -1 if not in one
        public string gangName { get; private set; }
        [DataMember]
        //Rank of the player in the gang 1-5, -1 if not in one
        public int gangRank { get; private set; }
        [DataMember]
        //Last time of login
        public DateTime lastActive { get; private set; }
        [DataMember]
        //Last time they were saved
        public DateTime lastUpdated { get; private set; }
        [DataMember]
        //Custom vehicle class array  with the players vehicle info
        public Vehicles[] civAir { get; private set; }
        [DataMember]
        //Custom vehicle class array  with the players vehicle info
        public Vehicles[] civCar { get; private set; }
        [DataMember]
        //Custom vehicle class array  with the players vehicle info
        public Vehicles[] civShip { get; private set; }
        [DataMember]
        //String list with their physical equipment
        public List<string> Equipment { get; private set; }
        [DataMember]
        //Custom item class for the palyers virtual items
        public VirtualItem[] Virtuals { get; private set; }
        [DataMember]
        //Because these ***holes wanted colors
        public int TargetLevel = -1;
        //Constructor
        public Player(int UID, long steamID, string name, int cash, int bank, int cop, int medic, int admin, int donator, string aliases,
            int kills, int deaths, int revives, int arrests, int timeC, int timeA, int timeM, int bountyC, int bountyW, string gangN, int gangR, long lastActive,
            string vCivAir, string vCivCar, string vCivShip, string gearCiv, long lastUpdated)
        {
            /*Initializing some variables*/
            this.aliases = new List<string>();
            this.Equipment = new List<string>();
            this.aliases.Clear();
            JArray vehiclesAir;
            JArray vehiclesCar;
            JArray vehiclesShip;
            JArray equipment;
            /*Assigining variables*/
            this.UID = UID;
            this.steamID = steamID;
            this.name = name;
            this.cash = cash;
            this.bank = bank;
            copLevel = cop;
            medicLevel = medic;
            adminLevel = admin;
            donatorLevel = donator;
            /* Splitting the data from the DB into segments*/
            string[] alias = aliases.Split(';');
            foreach (string aliasName in alias)
                if (aliasName.Length > 0)
                    this.aliases.Add(aliasName);
            //Assigning more variables
            this.kills = kills;
            this.deaths = deaths;
            this.medicRevives = revives;
            this.copArrest = arrests;
            this.timeCiv = timeC;
            this.timeApd = timeA;
            this.timeMed = timeM;
            this.bountyCollected = bountyC;
            this.bountyWanted = bountyW;
            this.gangName = gangN;
            this.gangRank = gangR;
            //Convert Unix time to C# DateTime
            this.lastActive = Helper.FromUnixTime(lastActive);
            this.lastUpdated = Helper.FromUnixTime(lastUpdated);
            //Parsing JSON Strings from the DB, Biggest pain in my ass. Thank you FeDot
            if (gearCiv.Length > 2)
            {
                gearCiv = gearCiv.Insert(0, "{\"civ_gear\": ");
                gearCiv += "}";
                equipment = JArray.Parse(JObject.Parse(gearCiv)["civ_gear"].ToString());
                for (int i = 0; i < equipment.Count(); i++)
                {
                    if (i < 5 || i > 5 && i < 9)
                    {
                        Equipment.Add(equipment[i].ToString());
                    }//Ammo
                    else if (i == 12)
                    {

                    }
                    else if (i == 15)
                    {
                        if (equipment[15].ToString().Length < 4) continue;
                        string virtualString = equipment[15].ToString();
                        virtualString = virtualString.Insert(0, "{\"Virtuals\": ");
                        virtualString += "}";
                        JArray tempV = JArray.Parse(JObject.Parse(virtualString)["Virtuals"].First.ToString());
                        Virtuals = new VirtualItem[tempV.Count];
                        int counter = 0;
                        foreach (JArray vi in tempV)
                        {
                            Virtuals[counter] = new VirtualItem { name = vi[0].ToString(), amount = (int)vi[1] };
                            counter++;
                        }
                    }
                }
            }
            if (vCivAir.Length > 2)
            {
                vCivAir = vCivAir.Insert(0, "{\"vehicle_civ_air\": ");
                vCivAir += "}";
                vehiclesAir = JArray.Parse(JObject.Parse(vCivAir)["vehicle_civ_air"].ToString());
                civAir = new Vehicles[vehiclesAir.Count];
                int vaCounter = 0;
                foreach (JObject vehicle in vehiclesAir)
                {
                    int ID = (int)vehicle["id"];
                    string vName = (string)vehicle["vehicle"];
                    int alive = (int)vehicle["alive"];
                    int active = (int)vehicle["active"];
                    int insured = (int)vehicle["modifications"]["insured"];
                    int turbo = (int)vehicle["modifications"]["turbo"];
                    int security = (int)vehicle["modifications"]["security"];
                    int storage = (int)vehicle["modifications"]["storage"];
                    civAir[vaCounter] = new Vehicles(ID, vName, alive, active, insured, turbo, security, storage);
                    vaCounter++;
                }
            }
            if (vCivCar.Length > 2)
            {
                vCivCar = vCivCar.Insert(0, "{\"vehicle_civ_car\": ");
                vCivCar += "}";
                vehiclesCar = JArray.Parse(JObject.Parse(vCivCar)["vehicle_civ_car"].ToString());
                civCar = new Vehicles[vehiclesCar.Count];
                int vcCounter = 0;
                foreach (JObject vehicle in vehiclesCar)
                {
                    int ID = (int)vehicle["id"];
                    string vName = (string)vehicle["vehicle"];
                    int alive = (int)vehicle["alive"];
                    int active = (int)vehicle["active"];
                    int insured = (int)vehicle["modifications"]["insured"];
                    int turbo = (int)vehicle["modifications"]["turbo"];
                    int security = (int)vehicle["modifications"]["security"];
                    int storage = (int)vehicle["modifications"]["storage"];
                    civCar[vcCounter] = new Vehicles(ID, vName, alive, active, insured, turbo, security, storage);
                    vcCounter++;
                }
            }
            if (vCivShip.Length > 2)
            {
                vCivShip = vCivShip.Insert(0, "{\"vehicle_civ_ship\": ");
                vCivShip += "}";
                vehiclesShip = JArray.Parse(JObject.Parse(vCivShip)["vehicle_civ_ship"].ToString());
                civShip = new Vehicles[vehiclesShip.Count];
                int vsCounter = 0;
                foreach (JObject vehicle in vehiclesShip)
                {
                    int ID = (int)vehicle["id"];
                    string vName = (string)vehicle["vehicle"];
                    int alive = (int)vehicle["alive"];
                    int active = (int)vehicle["active"];
                    int insured = (int)vehicle["modifications"]["insured"];
                    int turbo = (int)vehicle["modifications"]["turbo"];
                    int security = (int)vehicle["modifications"]["security"];
                    int storage = (int)vehicle["modifications"]["storage"];
                    civShip[vsCounter] = new Vehicles(ID, vName, alive, active, insured, turbo, security, storage);
                    vsCounter++;
                }
            }

        }
    }
}
