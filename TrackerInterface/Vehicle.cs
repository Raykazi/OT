using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace TrackerInterface
{
    /// <summary>
    /// Class for vehicles owned by the player.
    /// </summary>
    [DataContract]
    public class Vehicle
    {
        //Vehicle ID
        [DataMember]
        public int Id { get; private set; }
        //Vehicle name
        [DataMember]
        public string Name { get; private set; }
        //If the vehicle is alive or not??
        [DataMember]
        public int Alive { get; private set; }
        [DataMember]
        //If the vehicle is out on the map right now
        public int Active { get; private set; }
        [DataMember]
        //1 Basic Insurance 2 Full Insurance
        public int InsuranceLevel { get; private set; }
        [DataMember]
        //Tier 1-4, speed and manueverability level
        public int TurboLevel { get; private set; }
        [DataMember]
        //1 or 2 Security level for the car
        public int SecLevel { get; private set; }
        [DataMember]
        //1-4 Space of the vehicle
        public int StorageLevel { get; private set; }
        public int TargetLevel { get; set; } = -1;
        [DataMember]
        public List<Item> Inventory { get; private set; } //Custom item class for the palyers virtual items
        //Vehicle Constructor
        public Vehicle(int id, string name, int active, int insuranceLevel, int turboLevel, int secLevel, int storageLevel, List<Item> inventory)
        {
            Id = id;
            Name = name;
            Active = active;
            InsuranceLevel = insuranceLevel;
            TurboLevel = turboLevel;
            SecLevel = secLevel;
            StorageLevel = storageLevel;
            Inventory = inventory;
        }
        public static string TranslateName(string classname)
        {
            string actual = "";
            switch (classname)
            {
                case "B_APC_Wheeled_03_cannon_F":
                    actual = "AFV-4 Gorgon";
                    break;
                case "B_G_Van_02_transport_F":
                    actual = "Van Transport	";
                    break;
                case "B_G_Van_02_vehicle_F":
                    actual = "Van (Cargo)";
                    break;
                case "O_Heli_Light_02_unarmed_F":
                    actual = "PO-30 Orca (Unarmed)";
                    break;
                case "O_Heli_Transport_04_F":
                    actual = "Mi-290 Taru";
                    break;
                case "O_Heli_Transport_04_fuel_F":
                    actual = "Mi-290 Taru (Fuel)";
                    break;
                case "B_Boat_Transport_01_F":
                    actual = "Assault Boat";
                    break;
                case "B_G_Offroad_01_armed_F":
                    actual = "Offroad (Armed)";
                    break;
                case "B_G_Offroad_01_F":
                    actual = "Offroad";
                    break;
                case "B_Heli_Light_01_F":
                    actual = "MH-9 Hummingbird";
                    break;
                case "B_Heli_Transport_01_F":
                case "B_Heli_Transport_01_camo_F":
                    actual = "UH-80 Ghost Hawk";
                    break;
                case "B_Heli_Transport_03_F":
                case "B_Heli_Transport_03_black_F":
                    actual = "CH-67 Huron (Armed)";
                    break;
                case "B_Heli_Transport_03_unarmed_F":
                case "B_Heli_Transport_03_unarmed_green_F":
                    actual = "CH-67 Huron";
                    break;
                case "B_Lifeboat":
                    actual = "Rescue Boat";
                    break;
                case "B_MRAP_01_F":
                    actual = "Hunter";
                    break;
                case "B_Plane_CAS_01_F":
                    actual = "A-164 Wipeout";
                    break;
                case "B_Plane_Fighter_01_F":
                    actual = "F/A-181 Black Wasp II";
                    break;
                case "B_Quadbike_01_F":
                    actual = "Quad Bike";
                    break;
                case "O_SDV_01_F":
                case "B_SDV_01_F":
                    actual = "SDV";
                    break;
                case "B_Truck_01_box_F":
                    actual = "HEMTT Box";
                    break;
                case "B_Truck_01_fuel_F":
                    actual = "HEMTT Fuel";
                    break;
                case "B_Truck_01_transport_F":
                    actual = "HEMTT Transport";
                    break;
                case "B_T_APC_Wheeled_01_cannon_F":
                    actual = "AMV-7 Marshall";
                    break;
                case "B_T_LSV_01_armed_F":
                    actual = "Prowler (HMG)";
                    break;
                case "B_T_VTOL_01_infantry_F":
                    actual = "V-44 X Blackfish (Infantry)";
                    break;
                case "B_T_VTOL_01_vehicle_F":
                    actual = "V-44 X Blackfish (Vehicle)";
                    break;
                case "C_Boat_Civil_01_police_F":
                case "C_Boat_Civil_01_rescue_F":
                case "C_Boat_Civil_01_F":
                    actual = "Motorboat";
                    break;
                case "C_Boat_Transport_02_F":
                    actual = "RHIB";
                    break;
                case "C_Hatchback_01_F":
                    actual = "Hatchback";
                    break;
                case "C_Hatchback_01_sport_F":
                    actual = "Hatchback (Sport)";
                    break;
                case "C_Heli_Light_01_civil_F":
                    actual = "M-900";
                    break;
                case "C_Kart_01_Red_F":
                case "C_Kart_01_Vrana_F":
                case "C_Kart_01_Fuel_F":
                case "C_Kart_01_F":
                case "C_Kart_01_Blu_F":
                    actual = "Kart";
                    break;
                case "C_Plane_Civil_01_F":
                    actual = "Caesar BTT";
                    break;
                case "C_Plane_Civil_01_racing_F":
                    actual = "Caesar BTT (Racing)";
                    break;
                case "C_Rubberboat":
                    actual = "Rescue Boat";
                    break;
                case "C_Scooter_Transport_01_F":
                    actual = "Water Scooter";
                    break;
                case "C_SUV_01_F":
                    actual = "SUV";
                    break;
                case "C_Van_01_box_F":
                    actual = "Truck Boxer";
                    break;
                case "C_Van_01_fuel_F":
                    actual = "Truck Fuel";
                    break;
                case "C_Van_01_transport_F":
                    actual = "Truck";
                    break;
                case "C_Van_02_medevac_F":
                    actual = "Van (Ambulance)";
                    break;
                case "C_Van_02_transport_F":
                    actual = "Van Transport";
                    break;
                case "C_Van_02_vehicle_F":
                    actual = "Van (Cargo)";
                    break;
                case "I_C_Offroad_02_LMG_F":
                    actual = "MB 4WD (LMG)";
                    break;
                case "I_G_Offroad_01_AT_F":
                    actual = "Offroad (AT)";
                    break;
                case "I_Heli_light_03_unarmed_F":
                    actual = "WY-55 Hellcat";
                    break;
                case "I_Heli_Transport_02_F":
                    actual = "CH-49 Mohawk";
                    break;
                case "I_MRAP_03_F":
                    actual = "Strider";
                    break;
                case "I_Plane_Fighter_03_CAS_F":
                    actual = "A-143 Buzzard";
                    break;
                case "I_Plane_Fighter_04_F":
                    actual = "A-149 Gryphon";
                    break;
                case "I_Truck_02_covered_F":
                    actual = "Zamak Transport (Covered)";
                    break;
                case "I_Truck_02_fuel_F":
                    actual = "Zamak Fuel";
                    break;
                case "I_Truck_02_transport_F":
                    actual = "Zamak Transport";
                    break;
                case "O_Heli_Transport_04_bench_F":
                    actual = "Mi-290 Taru (Bench)";
                    break;
                case "O_Heli_Transport_04_covered_F":
                    actual = "Mi-290 Taru (Transport)";
                    break;
                case "O_Heli_Transport_04_repair_F":
                    actual = "Mi-290 Taru (Repair)";
                    break;
                case "O_LSV_02_unarmed_viper_F":
                    actual = "Qilin";
                    break;
                case "O_MRAP_02_F":
                    actual = "Ifrit";
                    break;
                case "O_Plane_CAS_02_F":
                    actual = "To-199 Neophron";
                    break;
                case "O_Plane_Fighter_02_F":
                    actual = "To-201 Shikra";
                    break;
                case "O_Truck_03_covered_F":
                    actual = "Tempest Transport (Covered)";
                    break;
                case "O_Truck_03_device_F":
                    actual = "Tempest (Device)";
                    break;
                case "O_Truck_03_fuel_F":
                    actual = "Tempest Fuel";
                    break;
                case "O_Truck_03_transport_F":
                    actual = "Tempest Transport";
                    break;
                case "O_T_LSV_02_armed_F":
                    actual = "Qilin (Minigun)";
                    break;
                case "O_T_VTOL_02_infantry_F":
                    actual = "Y-32 Xi'an (Infantry)";
                    break;
                case "O_T_VTOL_02_vehicle_F":
                    actual = "Y-32 Xi'an (Vehicle)";
                    break;


                case "C_Offroad_01_comms_F":
                case "C_Offroad_01_covered_F":
                case "C_Offroad_01_F":
                case "C_Offroad_01_repair_F":
                case "C_Offroad_02_unarmed_F":
                case "C_Tractor_01_F":
                default:
                    actual = classname;
                    break;
            }
            return actual;
        }
    }
}
