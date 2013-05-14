using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace ImranB.ModelBindingFix.WebApi.SystemWebHttp
{
    public class FixedRouteDataValueProviderFactory : RouteDataValueProviderFactory
    {
        private const string RequestLocalStorageKey = "{C0E50671-A1D4-429E-93C9-2AA63779924F}";

        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw Error.ArgumentNull("actionContext");
            }

            // cache the route provider across requests so that we don't recompute on every parameter.
            FixedRouteDataValueProvider provider;
            IDictionary<string, object> storage = actionContext.Request.Properties;

            if (!storage.TryGetValue(RequestLocalStorageKey, out provider))
            {
                provider = new FixedRouteDataValueProvider(actionContext, CultureInfo.InvariantCulture);
                storage[RequestLocalStorageKey] = provider;
            }

            return provider;       
        }
    }
}
