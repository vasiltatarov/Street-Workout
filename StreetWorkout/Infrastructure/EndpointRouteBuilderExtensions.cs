namespace StreetWorkout.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    using Controllers;

    public static class EndpointRouteBuilderExtensions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        public static void MapDefaultRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                "Workout Details",
                "/Workouts/Details/{id}/{information}",
                defaults: new
                {
                    controller = typeof(WorkoutsController).GetControllerName(),
                    action = nameof(WorkoutsController.Details),
                });

            endpoints.MapControllerRoute(
                "User Account",
                "/Accounts/Account/{username}",
                defaults: new
                {
                    controller = typeof(AccountsController).GetControllerName(),
                    action = nameof(AccountsController.Account)
                });
        }
    }
}
