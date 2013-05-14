using ImranB.ModelBindingFix.Mvc.SystemWebMvc;
using System.Web.Mvc;

namespace ImranB.ModelBindingFix.Mvc
{
    public static class Fixer
    {

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
    }
}
