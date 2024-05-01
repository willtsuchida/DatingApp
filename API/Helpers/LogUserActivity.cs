using Microsoft.AspNetCore.Mvc.Filters;

namespace API;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) //we can do something before or after next
    {
        var resultContext = await next(); // vai dar o action executed action --after executed

        //only if the user is authenticated, checkk
        //users must be logged to access controller, line below may not be necessary.
        if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        var userId = resultContext.HttpContext.User.GetUserId();

        var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var user = await repo.GetUserByIdAsync(int.Parse(userId));
        user.LastActive = DateTime.UtcNow;
        await repo.SaveAllAsync();

    }
}
