using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace ThFnsc.NFe.Infra.Services.Hangfire
{
    public class AllowAllAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context) => true;
    }
}
