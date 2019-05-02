using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DestinyWishlisterModels
{
    public class WeaponType
    {
        [JsonIgnore]
        public string Id => Name.ToLower().Replace(" ", "-");
        public string Name { get; set; }
        public List<WeaponSubtype> Subtypes { get; set; }

        [JsonIgnore]
        public EventCallback Show { get; set; }
        [JsonIgnore]
        public EventCallback Hide { get; set; }
    }
}