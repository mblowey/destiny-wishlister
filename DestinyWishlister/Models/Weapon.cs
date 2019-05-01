using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyWishlister.Models
{
    public class WeaponSocket
    {
        public string Name { get; set; }
        public long[] Perks { get; set; }
    }

    public class Weapon
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }
        public long Hash { get; set; }

        public WeaponSocket Slot1 { get; set; }
        public WeaponSocket Slot2 { get; set; }
        public WeaponSocket Slot3 { get; set; }
        public WeaponSocket Slot4 { get; set; }
    }
}
