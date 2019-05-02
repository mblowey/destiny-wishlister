using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonDataCreator
{

    public partial class InventoryItem
    {

        // Returns true if the InventoryItem is a legendary item (purple background)
        public bool IsLegendary => Inventory.TierType == 5;

        #region Weapon Queries
        /*
         * Weapon Queries 
         */

        // Returns true if the weapon is a pinnacle weapon. 
        public bool IsPinnacleWeapon => IsWeapon && WeaponPerkSockets.Any() && WeaponPerkSockets.All(s => s.RandomizedPlugItems.Count == 0 && s.ReusablePlugItems.Count == 1);

        // Returns true if the InventoryItem is a weapon
        public bool IsWeapon => ItemType == 3 && Enum.IsDefined(typeof(WeaponTypeId), ItemSubType);

        // Returns the hash of the InventoryItem's subtype if the item is a weapon. 
        public long SubtypeHash => Sockets.SocketEntries[0].SingleInitialItemHash;

        // Returns all the SocketEntries that belong to the WeaponPerk socket category.
        public IEnumerable<SocketEntry> AllWeaponPerkSockets => from index in this.GetSocketCategoryIndexes(SocketCategoryHash.WeaponPerks)
                                                                select Sockets.SocketEntries[index];

        // Returns the SocketEntries that belong to the WeaponPerk socket category, but the intrinsic and tracker sockets are removed
        public IEnumerable<SocketEntry> WeaponPerkSockets => from index in this.GetSocketCategoryIndexes(SocketCategoryHash.WeaponPerks)
                                                             where Sockets.SocketEntries[index].SocketTypeHash != (long)SocketTypeHash.WeaponIntrinsic &&
                                                                   Sockets.SocketEntries[index].SocketTypeHash != (long)SocketTypeHash.MasterworkTracker
                                                             select Sockets.SocketEntries[index];
        #endregion

        #region Armor Queries
        /*
         * 
         * Armor Queries
         * 
         */

        // Returns true if the InventoryItem is a piece of armor.
        public bool IsArmor => ItemType == 2 && Enum.IsDefined(typeof(ArmorTypeId), ItemSubType);
        #endregion
    }

    public static class InventoryItemExtensions
    {

        // Returns a list of indexes for the provided SocketCategory. These indexes should then be used in the 
        // InventoryItem.Sockets.SocketEntries list to get the sockets that match the provided category.
        public static List<int> GetSocketCategoryIndexes(this InventoryItem Item, SocketCategoryHash category)
        {
            var socketCategory = Item.Sockets.SocketCategories.FirstOrDefault(s => s.SocketCategoryHash == (long)category);

            return socketCategory?.SocketIndexes?.Select(i => (int)i)?.ToList() ?? new List<int>();
        }

        public static IEnumerable<SocketEntry> GetWeaponPerkSockets(this InventoryItem Item)
        {
            var socketIndexes = Item.GetSocketCategoryIndexes(SocketCategoryHash.WeaponPerks);

            foreach (var index in socketIndexes)
            {
                yield return Item.Sockets.SocketEntries[index];
            }
        }
    }

    public partial class SocketEntry
    {
        public string Name => Program.Lookup(SingleInitialItemHash)?.ItemTypeDisplayName ?? Program.Lookup(RandomizedPlugItems[0].PlugItemHash).ItemTypeDisplayName;

        public List<long> PerkHashes
        {
            get
            {
                var reusablePlugPerks = ReusablePlugItems.Select(p => p.PlugItemHash);
                var randomizedPlugPerks = RandomizedPlugItems.Select(p => p.PlugItemHash);

                return reusablePlugPerks.Concat(randomizedPlugPerks).ToHashSet().ToList();
            }
        }
    }
}
