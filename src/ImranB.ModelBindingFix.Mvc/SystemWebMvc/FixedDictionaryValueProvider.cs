using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace ImranB.ModelBindingFix.Mvc.SystemWebMvc
{
    public class FixedDictionaryValueProvider<TValue> : IValueProvider
    {
        private readonly Lazy<FixedPrefixContainer> _prefixContainer;
        private readonly Dictionary<string, ValueProviderResult> _values = new Dictionary<string, ValueProviderResult>(StringComparer.OrdinalIgnoreCase);

        public FixedDictionaryValueProvider(IDictionary<string, TValue> dictionary, CultureInfo culture)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            foreach (KeyValuePair<string, TValue> entry in dictionary)
            {
                object rawValue = entry.Value;
                string attemptedValue = Convert.ToString(rawValue, culture);
                _values[entry.Key] = new ValueProviderResult(rawValue, attemptedValue, culture);
            }

            _prefixContainer = new Lazy<FixedPrefixContainer>(() => new FixedPrefixContainer(_values.Keys), isThreadSafe: true);
        }

        public virtual bool ContainsPrefix(string prefix)
        {
            return _prefixContainer.Value.ContainsPrefix(prefix);
        }

        public virtual ValueProviderResult GetValue(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            ValueProviderResult valueProviderResult;
            _values.TryGetValue(key, out valueProviderResult);
            return valueProviderResult;
        }

        public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
        {
            return _prefixContainer.Value.GetKeysFromPrefix(prefix);
        }
    }
}
