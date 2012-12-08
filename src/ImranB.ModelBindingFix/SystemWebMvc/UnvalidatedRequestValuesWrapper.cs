using System.Collections.Specialized;
using System.Web.Helpers;

namespace ImranB.ModelBindingFix.SystemWebMvc
{
    internal sealed class UnvalidatedRequestValuesWrapper : IUnvalidatedRequestValues
    {
        private readonly UnvalidatedRequestValues _unvalidatedValues;

        public UnvalidatedRequestValuesWrapper(UnvalidatedRequestValues unvalidatedValues)
        {
            _unvalidatedValues = unvalidatedValues;
        }

        public NameValueCollection Form
        {
            get { return _unvalidatedValues.Form; }
        }

        public NameValueCollection QueryString
        {
            get { return _unvalidatedValues.QueryString; }
        }

        public string this[string key]
        {
            get { return _unvalidatedValues[key]; }
        }
    }
}
