using System.Collections.Specialized;

namespace ImranB.ModelBindingFix.Mvc.SystemWebMvc
{
    internal interface IUnvalidatedRequestValues
    {
        NameValueCollection Form { get; }
        NameValueCollection QueryString { get; }
        string this[string key] { get; }
    }
}
