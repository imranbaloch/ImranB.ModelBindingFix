using System.Collections.Specialized;

namespace ImranB.ModelBindingFix.SystemWebMvc
{
    internal interface IUnvalidatedRequestValues
    {
        NameValueCollection Form { get; }
        NameValueCollection QueryString { get; }
        string this[string key] { get; }
    }
}
