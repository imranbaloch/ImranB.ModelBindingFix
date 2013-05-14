using System.Globalization;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace ImranB.ModelBindingFix.WebApi.SystemWebHttp
{
    public class FixedQueryStringValueProvider : FixedNameValuePairsValueProvider
    {
        public FixedQueryStringValueProvider(HttpActionContext actionContext, CultureInfo culture)
            : base(() => actionContext.ControllerContext.Request.GetQueryNameValuePairs(), culture)
        {
        }
    }
}
