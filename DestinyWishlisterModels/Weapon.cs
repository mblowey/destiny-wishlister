using System.Collections.Generic;

namespace DestinyWishlisterModels
{
    


    public class Weapon
    {

        public class WeaponSocket
        {
            public string Name { get; set; }
            public List<long> Perks { get; set; }
        }

        public string Name { get; set; }
        public long Hash { get; set; }
        public List<WeaponSocket> Sockets { get; set; }


        public string WeaponType { get; set; }
        public string WeaponSubtype { get; set; }
    }
}
