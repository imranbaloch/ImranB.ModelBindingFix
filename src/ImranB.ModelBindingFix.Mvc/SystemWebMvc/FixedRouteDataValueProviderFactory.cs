using System;
using System.Web.Mvc;

namespace ImranB.ModelBindingFix.Mvc.SystemWebMvc
{
    public sealed class FixedRouteDataValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            return new FixedRouteDataValueProvider(controllerContext);
        }
    }
}
