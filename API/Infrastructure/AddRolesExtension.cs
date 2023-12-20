namespace API.Infrastructure
{
    using Microsoft.AspNetCore.Identity;

    using static Common.GeneralApplicationConstants;

    public static class AddRolesExtension
    {
        public static IApplicationBuilder SeedRoles(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope =
                app.ApplicationServices.CreateScope();

            var serviceProvider = serviceScope.ServiceProvider;
            var roleManager = serviceProvider.
                GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                string[] roleNames = { MarketplaceRoleName, MatchingRoleName};

                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        var role = new IdentityRole<Guid>(roleName);
                        await roleManager.CreateAsync(role);
                    }
                }
            })
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
