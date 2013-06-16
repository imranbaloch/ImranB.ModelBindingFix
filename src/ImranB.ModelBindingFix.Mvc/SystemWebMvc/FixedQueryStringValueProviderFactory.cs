using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ImranB.ModelBindingFix.Mvc.SystemWebMvc
{
    public sealed class FixedQueryStringValueProviderFactory : ValueProviderFactory
    {
        private readonly UnvalidatedRequestValuesAccessor _unvalidatedValuesAccessor;

        public FixedQueryStringValueProviderFactory()
            : this(null)
        {
        }

        // For unit testing
        internal FixedQueryStringValueProviderFactory(UnvalidatedRequestValuesAccessor unvalidatedValuesAccessor)
        {
            _unvalidatedValuesAccessor = unvalidatedValuesAccessor ?? (cc => new UnvalidatedRequestValuesWrapper(cc.HttpContext.Request.Unvalidated()));
        }

        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            return new FixedQueryStringValueProvider(controllerContext, _unvalidatedValuesAccessor(controllerContext));
        }
    }
}
