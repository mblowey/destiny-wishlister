using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using DestinyWishlisterModels;
using System.IO;
using Newtonsoft.Json;

namespace JsonDataCreator
{

    public static class Program
    {
        static List<InventoryItem> Items { get; set; }
        public static InventoryItem Lookup(long itemHash)
        {
            return Items.FirstOrDefault(i => i.Hash == itemHash);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Downloading Json.");
            var json = new WebClient().DownloadString("https://destiny.plumbing/en/raw/DestinyInventoryItemDefinition.json");

            Console.WriteLine("Parsing Json.");
            Items = InventoryItem.FromJson(json).Values.ToList();

            Console.WriteLine("Processing.");

            // Create Weapon objects out of the qualifying InventoryItems
            var Weapons = from item in Items
                          where item.IsWeapon && item.IsLegendary && item.WeaponPerkSockets.Count() == 4
                          orderby item.DisplayProperties.Name
                          select CreateWeapon(item);

            Console.WriteLine($"Created {Weapons.Count()} weapons.");

            // Build out the list of WeaponTypes to be serialized
            var WeaponTypes = new List<WeaponType>();
            foreach (var weapon in Weapons)
            {
                var subtype = GetWeaponSubtype(WeaponTypes, weapon);

                foreach (var weaponSocket in weapon.Sockets)
                {
                    var subtypeSocket = GetSocket(subtype.Sockets, weaponSocket);

                    subtypeSocket.AddPerks(weaponSocket.Perks);
                }
            }

            SortWeaponTypes(ref WeaponTypes);

            // Write out WeaponType json files and delete previous weapon json files
            Console.WriteLine("Writing WeaponType json files.");

            Directory.CreateDirectory("data");
            foreach (var weaponType in WeaponTypes)
            {
                var filename = Path.Combine("data", "weapon-type." + weaponType.Id + ".json");

                File.WriteAllText(filename, JsonConvert.SerializeObject(weaponType, Formatting.None));

                Console.WriteLine($"\"{filename.Replace("\\", "/")}\",");
            }


            // Write out all Weapons to json files
            Console.WriteLine("Writing weapon json files");

            var weaponJsonByType = new Dictionary<string, string>();
            foreach (var weapon in Weapons)
            {
                var filename = Path.Combine("data", "weapons." + weapon.WeaponType.ToLower().Replace(" ", "-") + ".json");

                if (!weaponJsonByType.ContainsKey(filename))
                    weaponJsonByType[filename] = "[";

                weaponJsonByType[filename] += JsonConvert.SerializeObject(weapon, Formatting.None) + ",";
            }

            foreach (var (filename, jsonText) in weaponJsonByType)
            {
                File.WriteAllText(filename, jsonText + "]");

                Console.WriteLine($"\"{filename.Replace("\\", "/")}\",");
            }

        }

        public static Weapon CreateWeapon(InventoryItem item)
        {
            Weapon weapon = new Weapon();

            weapon.Name = item.DisplayProperties.Name;
            weapon.Hash = item.Hash;

            weapon.WeaponType = item.ItemTypeDisplayName;
            weapon.WeaponSubtype = Lookup(item.SubtypeHash).DisplayProperties.Name;

            weapon.Sockets = new List<Weapon.WeaponSocket>();

            foreach (var socket in item.WeaponPerkSockets)
            {
                weapon.Sockets.Add(new Weapon.WeaponSocket
                {
                    Name = socket.Name,
                    Perks = socket.PerkHashes
                });
            }


            return weapon;
        }

        // Gets the WeaponSubtype from weaponTypes that corresponds to weapon. Creates WeaponTypes and WeaponSubtypes as needed.
        public static WeaponSubtype GetWeaponSubtype(List<WeaponType> weaponTypes, Weapon weapon)
        {
            var weaponType = weaponTypes.Find(wt => wt.Name == weapon.WeaponType);

            if (weaponType == null)
            {
                weaponTypes.Add(new WeaponType { Name = weapon.WeaponType, Subtypes = new List<WeaponSubtype>() });
                weaponType = weaponTypes.Last();
            }

            var weaponSubtype = weaponType.Subtypes.Find(st => st.Name == weapon.WeaponSubtype);

            if (weaponSubtype == null)
            {
                weaponType.Subtypes.Add(new WeaponSubtype { Name = weapon.WeaponSubtype, Sockets = new List<Socket>() });
                weaponSubtype = weaponType.Subtypes.Last();
            }

            return weaponSubtype;
        }

        public static Socket GetSocket(List<Socket> sockets, Weapon.WeaponSocket weaponSocket)
        {
            var subtypeSocket = sockets.Find(s => s.Name == weaponSocket.Name);

            if (subtypeSocket == null)
            {
                sockets.Add(new Socket { Name = weaponSocket.Name, Perks = new List<DestinyWishlisterModels.Perk>() });
                subtypeSocket = sockets.Last();
            }

            return subtypeSocket;
        }

        public static void AddPerks(this Socket socket, List<long> perkHashes)
        {
            var newPerkHashes = perkHashes.Where(perkHash => !socket.Perks.Any(perk => perk.Hash == perkHash));

            socket.Perks.AddRange(newPerkHashes.Select(CreatePerk));
        }

        public static DestinyWishlisterModels.Perk CreatePerk(long perkHash)
        {
            var perkItem = Lookup(perkHash);

            return new DestinyWishlisterModels.Perk
            {
                Hash = perkHash,
                IconUrl = "https://www.bungie.net" + perkItem.DisplayProperties.Icon,
                IsSelected = false,
                Name = perkItem.DisplayProperties.Name
            };
        }

        public static void SortWeaponTypes(ref List<WeaponType> weaponTypes)
        {
            foreach (var weaponType in weaponTypes)
            {
                foreach (var weaponSubtype in weaponType.Subtypes)
                {
                    foreach (var socket in weaponSubtype.Sockets)
                    {
                        socket.Perks = socket.Perks.OrderBy(p => p.Name).ToList();
                    }

                    weaponSubtype.Sockets = weaponSubtype.Sockets.OrderBy(s => s.Name).ToList();
                }

                weaponType.Subtypes = weaponType.Subtypes.OrderBy(st => st.Name).ToList();
            }

            weaponTypes = weaponTypes.OrderBy(wt => wt.Name).ToList();
        }
    }
}
