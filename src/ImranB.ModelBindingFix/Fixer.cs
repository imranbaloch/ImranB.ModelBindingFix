using ImranB.ModelBindingFix.SystemWebMvc;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace ImranB.ModelBindingFix
{
    public static class Fixer
    {
        public static void FixModelBindingIssue()
        {
            FixMvcModelBindingIssue();
            FixWebApiModelBindingIssue();
        }

        public static void FixMvcModelBindingIssue()
        {
            ValueProviderFactories.Factories.Clear();
            var factories = new ValueProviderFactoryCollection()
            {
                new FixedChildActionValueProviderFactory(),
                new FixedFormValueProviderFactory(),
                new FixedJsonValueProviderFactory(),
                new FixedRouteDataValueProviderFactory(),
                new FixedQueryStringValueProviderFactory(),
                new FixedHttpFileCollectionValueProviderFactory()
            };
            foreach (var factory in factories)
            {
                ValueProviderFactories.Factories.Add(factory);
            }
        }

        public static void FixWebApiModelBindingIssue()
        {
            GlobalConfiguration.Configuration.Services.RemoveAll(typeof(System.Web.Http.ValueProviders.ValueProviderFactory), _ => true);
            var list = new List<System.Web.Http.ValueProviders.ValueProviderFactory> 
            {
                new ImranB.ModelBindingFix.SystemWebHttp.FixedQueryStringValueProviderFactory(),
                new ImranB.ModelBindingFix.SystemWebHttp.FixedRouteDataValueProviderFactory()
            };
            GlobalConfiguration.Configuration.Services.AddRange(typeof(System.Web.Http.ValueProviders.ValueProviderFactory), list);
        }
    }
}
