using System;
using System.Web.Mvc;
using System.Web.Helpers;

namespace ImranB.ModelBindingFix.SystemWebMvc
{
   public sealed class FixedFormValueProviderFactory : ValueProviderFactory
    {
        private readonly UnvalidatedRequestValuesAccessor _unvalidatedValuesAccessor;

        public FixedFormValueProviderFactory()
            : this(null)
        {
        }

        // For unit testing
        internal FixedFormValueProviderFactory(UnvalidatedRequestValuesAccessor unvalidatedValuesAccessor)
        {
            _unvalidatedValuesAccessor = unvalidatedValuesAccessor ?? (cc => new UnvalidatedRequestValuesWrapper(cc.HttpContext.Request.Unvalidated()));
        }

        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            return new FixedFormValueProvider(controllerContext, _unvalidatedValuesAccessor(controllerContext));
        }
   }
}
