using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyWishlister.Models
{
    public class Perk
    {
        public long Hash { get; set; }
        public string IconUrl { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
