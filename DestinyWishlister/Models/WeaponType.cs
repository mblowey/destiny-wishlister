using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyWishlister.Models
{
    public class WeaponType
    {
        public string Id => Name.ToLower().Replace(" ", "-");
        public string Name { get; set; }
        public WeaponSubtype[] Subtypes { get; set; }


        public EventCallback Show { get; set; }
        public EventCallback Hide { get; set; }
    }
}
