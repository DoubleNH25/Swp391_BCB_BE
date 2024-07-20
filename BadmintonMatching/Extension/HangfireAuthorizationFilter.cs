using Hangfire.Dashboard;

namespace BadmintonMatching.Extension
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string[] _roles;

        public HangfireAuthorizationFilter(params string[] roles)
        {
            _roles = roles;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = ((AspNetCoreDashboardContext)context).HttpContext;

            return true;
        }
    }
}
