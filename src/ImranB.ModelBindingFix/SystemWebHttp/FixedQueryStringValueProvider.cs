using System.Globalization;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace ImranB.ModelBindingFix.SystemWebHttp
{
    public class FixedQueryStringValueProvider : FixedNameValuePairsValueProvider
    {
        public FixedQueryStringValueProvider(HttpActionContext actionContext, CultureInfo culture)
            : base(() => actionContext.ControllerContext.Request.GetQueryNameValuePairs(), culture)
        {
        }
    }
}
