using System;
using System.Globalization;
using System.Web.Mvc;

namespace ImranB.ModelBindingFix.SystemWebMvc
{
    public sealed class FixedChildActionValueProvider : FixedDictionaryValueProvider<object>
    {
        private static string _childActionValuesKey = Guid.NewGuid().ToString();

        public FixedChildActionValueProvider(ControllerContext controllerContext)
            : base(controllerContext.RouteData.Values, CultureInfo.InvariantCulture)
        {
        }

        internal static string ChildActionValuesKey
        {
            get { return _childActionValuesKey; }
        }

        public override ValueProviderResult GetValue(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            ValueProviderResult explicitValues = base.GetValue(ChildActionValuesKey);
            if (explicitValues != null)
            {
                FixedDictionaryValueProvider<object> rawExplicitValues = explicitValues.RawValue as FixedDictionaryValueProvider<object>;
                if (rawExplicitValues != null)
                {
                    return rawExplicitValues.GetValue(key);
                }
            }

            return null;
        }
    }
}
