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


        public async Task<string> Create(WeaponTypeData data)
        {
            await Initialization;

            Console.WriteLine("Placeholder Wishlist Creation");
            return "";
        }
    }
}
