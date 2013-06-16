using System.Globalization;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ImranB.ModelBindingFix.Mvc.SystemWebMvc
{
    public sealed class FixedQueryStringValueProvider : FixedNameValueCollectionValueProvider
    {
        public FixedQueryStringValueProvider(ControllerContext controllerContext)
            : this(controllerContext, new UnvalidatedRequestValuesWrapper(controllerContext.HttpContext.Request.Unvalidated()))
        {
        }

        internal FixedQueryStringValueProvider(ControllerContext controllerContext, IUnvalidatedRequestValues unvalidatedValues)
            : base(controllerContext.HttpContext.Request.QueryString, unvalidatedValues.QueryString, CultureInfo.InvariantCulture)
        {
        }
    }
}
