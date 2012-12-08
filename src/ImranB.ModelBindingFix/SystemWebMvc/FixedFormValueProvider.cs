using System;
using System.Globalization;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ImranB.ModelBindingFix.SystemWebMvc
{
    public sealed class FixedFormValueProvider : FixedNameValueCollectionValueProvider
    {
        public FixedFormValueProvider(ControllerContext controllerContext)
            : this(controllerContext, new UnvalidatedRequestValuesWrapper(controllerContext.HttpContext.Request.Unvalidated()))
        {
        }

        internal FixedFormValueProvider(ControllerContext controllerContext, IUnvalidatedRequestValues unvalidatedValues)
            : base(controllerContext.HttpContext.Request.Form, unvalidatedValues.Form, CultureInfo.CurrentCulture)
        {
        }
    }
}
