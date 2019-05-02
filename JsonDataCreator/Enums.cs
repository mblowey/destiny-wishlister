using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JsonDataCreator
{

    //Values correspond to an InventoryItem's ItemSubtype
    //when its ItemType == 3
    public enum WeaponTypeId : long
    {
        [Description("Auto Rifle")]
        AutoRifle = 6,

        [Description("Shotgun")]
        Shotgun = 7,

        [Description("Machine Gun")]
        MachineGun = 8,

        [Description("Hand Cannon")]
        HandCannon = 9,

        [Description("Rocket Launcher")]
        RocketLauncher = 10,

        [Description("Fusion Rifle")]
        FusionRifle = 11,

        [Description("Sniper Rifle")]
        SniperRifle = 12,

        [Description("Pulse Rifle")]
        PulseRifle = 13,

        [Description("Scout Rifle")]
        ScoutRifle = 14,

        [Description("Sidearm")]
        Sidearm = 17,

        [Description("Sword")]
        Sword = 18,

        [Description("Linear Fusion Rifle")]
        LinearFusionRifle = 22,

        [Description("Grenade Launcher")]
        GrenadeLauncher = 23,

        [Description("Submachine Gun")]
        SubmachineGun = 24,

        [Description("Trace Rifle")]
        TraceRifle = 25,

        [Description("Combat Bow")]
        CombatBow = 31,
    }

    //Values correspond to an InventoryItem's ItemSubtype
    //when its ItemType == 2
    public enum ArmorTypeId : long
    {
        Helmet = 26,
        Gauntlets = 27,
        ChestArmor = 28,
        LegArmor = 29,
        HunterCloak = 30,
        TitanMark = 30,
        WarlockBond = 30,
    }

    public enum SocketCategoryHash : long
    {
        WeaponPerks = 4241085061,
        WeaponMods = 2685412949,

        ArmorPerks = 2518356194,
        ArmorMods = 590099826,
    }

    public enum SocketTypeHash : long
    {
        WeaponIntrinsic = 3956125808,
        MasterworkTracker = 1282012138,
    }
}
