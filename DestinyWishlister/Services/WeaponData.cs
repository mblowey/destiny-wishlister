using DestinyWishlisterModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DestinyWishlister.Services
{
    public class WeaponData
    {
        public static List<string> WeaponDataUris = new List<string>
        {
            "data/weapons.auto-rifle.json",
            "data/weapons.combat-bow.json",
            "data/weapons.fusion-rifle.json",
            "data/weapons.grenade-launcher.json",
            "data/weapons.hand-cannon.json",
            "data/weapons.linear-fusion-rifle.json",
            "data/weapons.machine-gun.json",
            "data/weapons.pulse-rifle.json",
            "data/weapons.rocket-launcher.json",
            "data/weapons.scout-rifle.json",
            "data/weapons.shotgun.json",
            "data/weapons.sidearm.json",
            "data/weapons.sniper-rifle.json",
            "data/weapons.submachine-gun.json",
            "data/weapons.sword.json",
        };

        public List<Weapon> Weapons { get; set; }

        public bool IsInit => Initialization.IsCompleted;
        public Task Initialization { get; set; }

        public WeaponData(HttpClient Http, WeaponTypeData weaponTypeData)
        {
            Weapons = new List<Weapon>();

            Initialization = InitializeAsync(Http, weaponTypeData);
        }

        private async Task InitializeAsync(HttpClient Http, WeaponTypeData weaponTypeData)
        {
            // Wait for WeaponTypeData to finish loading first so the UI can show faster.
            // Since weapons are only used in the background, we can wait.
            await weaponTypeData.Initialization;

            foreach (var uri in WeaponDataUris)
            {
                Weapons.AddRange(await Http.GetJsonAsync<List<Weapon>>(uri));
                Console.WriteLine($"Loaded {uri}");
            }

            Console.WriteLine($"Loaded {Weapons.Count} weapons.");
        }
    }
}
