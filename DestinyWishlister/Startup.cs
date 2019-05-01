using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using DestinyWishlister.Services;

namespace DestinyWishlister
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<EventCallbackFactory>();

            services.AddSingleton<WeaponTypeData>();
            services.AddSingleton<WishlistGenerator>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
