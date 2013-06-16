using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace ImranB.ModelBindingFix.WebApi.SystemWebHttp
{
    public class FixedQueryStringValueProviderFactory : QueryStringValueProviderFactory
    {
        private const string RequestLocalStorageKey = "{8572540D-3BD9-46DA-B112-A1E6C9086003}";

        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {

            if (actionContext == null)
            {
                throw Error.ArgumentNull("actionContext");
            }

            // Only parse the query string once-per request. 

            FixedQueryStringValueProvider provider;
            IDictionary<string, object> storage = actionContext.Request.Properties;

            if (!storage.TryGetValue(RequestLocalStorageKey, out provider))
            {
                provider = new FixedQueryStringValueProvider(actionContext, CultureInfo.InvariantCulture);
                storage[RequestLocalStorageKey] = provider;
            }

            return provider;
        }
    }

}
