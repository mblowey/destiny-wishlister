using DestinyWishlisterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyWishlister.Services
{
    public class WishlistGenerator
    {
        public WeaponData WeaponData { get; set; }

        public bool IsInit => Initialization.IsCompleted;
        public Task Initialization { get; set; }

        public WishlistGenerator(WeaponData weaponData)
        {
            WeaponData = weaponData;

            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Console.WriteLine("Starting WishlistGenerator.InitializeAsync");
            await WeaponData.Initialization;
            Console.WriteLine("Ending WishlistGenerator.InitializeAsync");
        }


        public async Task<string> Create(WeaponTypeData TypeData)
        {
            await Initialization;

            string wishlistText = "";

            foreach (var weapon in WeaponData.Weapons)
            {
                var subtype = TypeData.Types.Find(t => t.Name == weapon.WeaponType).Subtypes.Find(st => st.Name == weapon.WeaponSubtype);

                var perkHashesBySocket = new List<List<long>>();
                foreach (var weaponSocket in weapon.Sockets)
                {
                    var subtypeSocket = subtype.Sockets.Find(s => s.Name == weaponSocket.Name);
                    var selectedPerkHashes = GetSelectedPerkHashes(subtypeSocket.Perks);

                    if (selectedPerkHashes.Count == 0)
                        continue;

                    var matchingPerks = selectedPerkHashes.Where(ph => weaponSocket.Perks.Contains(ph)).ToList();
                    Console.WriteLine($"{weapon.Name} has {matchingPerks.Count} matching perks in socket {weaponSocket.Name}");
                    perkHashesBySocket.Add(matchingPerks);
                }

                List<string> combinations = GetPerkCombinations(perkHashesBySocket);

                foreach (var combo in combinations)
                {
                    wishlistText += $"dimwishlist:item={weapon.Hash}&perks={combo}\n";
                }
            }

            return wishlistText;
        }

        private List<long> GetSelectedPerkHashes(List<Perk> perks)
        {
            return perks.Where(p => p.IsSelected).Select(p => p.Hash).ToList();
        }

        private List<string> GetPerkCombinations(List<List<long>> perkHashesBySocket)
        {
            var combos = new List<string>();
            Console.WriteLine($"perkHashesBySocket.Count == {perkHashesBySocket.Count}");

            foreach (var socket in perkHashesBySocket)
            {
                var newCombos = new List<string>();

                foreach (var perk in socket)
                {

                    if (combos.Count == 0)
                    {
                        newCombos.Add(perk.ToString());
                    }
                    else
                    {
                        foreach (var combo in combos)
                        {
                            newCombos.Add(combo + "," + perk.ToString());
                        }
                    }
                }

                combos = newCombos;
            }

            return combos;
        }
    }
}
