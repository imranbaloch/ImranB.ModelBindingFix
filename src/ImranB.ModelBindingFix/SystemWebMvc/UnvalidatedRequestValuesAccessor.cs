using System.Web.Mvc;

namespace ImranB.ModelBindingFix.SystemWebMvc
{
    internal delegate IUnvalidatedRequestValues UnvalidatedRequestValuesAccessor(ControllerContext controllerContext);
}
