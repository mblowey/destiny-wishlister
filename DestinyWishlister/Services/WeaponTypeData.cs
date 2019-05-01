using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using DestinyWishlister.Models;

namespace DestinyWishlister.Services
{
    public class WeaponTypeData
    {
        public static List<string> DataURIs = new List<string>
        {
            "data/AutoRifle.json",
            "data/CombatBow.json",
            "data/FusionRifle.json",
            "data/GrenadeLauncher.json",
            "data/HandCannon.json",
            "data/LinearFusionRifle.json",
            "data/MachineGun.json",
            "data/PulseRifle.json",
            "data/RocketLauncher.json",
            "data/ScoutRifle.json",
            "data/Shotgun.json",
            "data/Sidearm.json",
            "data/SniperRifle.json",
            "data/SubmachineGun.json",
            "data/Sword.json",
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
            foreach (var uri in DataURIs)
            {
                Types.Add(await Http.GetJsonAsync<WeaponType>(uri));
                Console.WriteLine($"Added {Types.Last().Name} to WeaponTypeData.Types");
            }
        }
    }
}
