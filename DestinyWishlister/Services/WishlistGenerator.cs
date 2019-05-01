using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyWishlister.Services
{
    public class WishlistGenerator
    {
        public string Create(WeaponTypeData data)
        {
            var ret = "";

            foreach (var type in data.Types)
            {
                foreach (var subtype in type.Subtypes)
                {
                    foreach (var socket in subtype.Sockets)
                    {
                        foreach (var perk in socket.Perks)
                        {
                            if (perk.IsSelected)
                                ret += perk.Name + '\n';
                        }
                    }
                }
            }

            return ret;
        }
    }
}
