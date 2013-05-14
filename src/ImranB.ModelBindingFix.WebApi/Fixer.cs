using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ValueProviders;
using ImranB.ModelBindingFix.WebApi.SystemWebHttp;

namespace ImranB.ModelBindingFix.WebApi
{
    public static class Fixer
    {
        public static void FixWebApiModelBindingIssue()
        {
            GlobalConfiguration.Configuration.Services.RemoveAll(typeof (ValueProviderFactory), _ => true);
            var list = new List<ValueProviderFactory>
                           {
                               new FixedQueryStringValueProviderFactory(),
                               new FixedRouteDataValueProviderFactory()
                           };
            GlobalConfiguration.Configuration.Services.AddRange(typeof (ValueProviderFactory), list);
        }
    }
}