using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using DestinyWishlisterModels;

namespace DestinyWishlister.Services
{
    public class WeaponTypeData
    {
        public static List<string> WeaponTypeDataURIs = new List<string>
        {
            "data/weapon-type.auto-rifle.json",
            "data/weapon-type.combat-bow.json",
            "data/weapon-type.fusion-rifle.json",
            "data/weapon-type.grenade-launcher.json",
            "data/weapon-type.hand-cannon.json",
            "data/weapon-type.linear-fusion-rifle.json",
            "data/weapon-type.machine-gun.json",
            "data/weapon-type.pulse-rifle.json",
            "data/weapon-type.rocket-launcher.json",
            "data/weapon-type.scout-rifle.json",
            "data/weapon-type.shotgun.json",
            "data/weapon-type.sidearm.json",
            "data/weapon-type.sniper-rifle.json",
            "data/weapon-type.submachine-gun.json",
            "data/weapon-type.sword.json",
        };

        public List<WeaponType> Types { get; set; }

        public bool IsInit => Initialization.IsCompleted;
        public Task Initialization { get; set; }

        public WeaponTypeData(HttpClient Http)
        {
            Types = new List<WeaponType>();

            Initialization = InitializeAsync(Http);
        }

        private async Task InitializeAsync(HttpClient Http)
        {
            foreach (var uri in WeaponTypeDataURIs)
            {
                Types.Add(await Http.GetJsonAsync<WeaponType>(uri));
                Console.WriteLine($"Added {Types.Last().Name} to WeaponTypeData.Types");
            }
        }
    }
}
