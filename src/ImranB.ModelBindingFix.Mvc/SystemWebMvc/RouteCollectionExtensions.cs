using System;
using System.Web.Routing;

namespace ImranB.ModelBindingFix.Mvc.SystemWebMvc
{
    public static class RouteCollectionExtensions
    {
        // This method returns a new RouteCollection containing only routes that matched a particular area.
        // The Boolean out parameter is just a flag specifying whether any registered routes were area-aware.
        private static RouteCollection FilterRouteCollectionByArea(RouteCollection routes, string areaName, out bool usingAreas)
        {
            if (areaName == null)
            {
                areaName = String.Empty;
            }

            usingAreas = false;
            RouteCollection filteredRoutes = new RouteCollection();

            using (routes.GetReadLock())
            {
                foreach (RouteBase route in routes)
                {
                    string thisAreaName = AreaHelpers.GetAreaName(route) ?? String.Empty;
                    usingAreas |= (thisAreaName.Length > 0);
                    if (String.Equals(thisAreaName, areaName, StringComparison.OrdinalIgnoreCase))
                    {
                        filteredRoutes.Add(route);
                    }
                }
            }

            // if areas are not in use, the filtered route collection might be incorrect
            return (usingAreas) ? filteredRoutes : routes;
        }

        internal static VirtualPathData GetVirtualPathForArea(this RouteCollection routes, RequestContext requestContext, string name, RouteValueDictionary values, out bool usingAreas)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (!String.IsNullOrEmpty(name))
            {
                // the route name is a stronger qualifier than the area name, so just pipe it through
                usingAreas = false;
                return routes.GetVirtualPath(requestContext, name, values);
            }

            string targetArea = null;
            if (values != null)
            {
                object targetAreaRawValue;
                if (values.TryGetValue("area", out targetAreaRawValue))
                {
                    targetArea = targetAreaRawValue as string;
                }
                else
                {
                    // set target area to current area
                    if (requestContext != null)
                    {
                        targetArea = AreaHelpers.GetAreaName(requestContext.RouteData);
                    }
                }
            }

            // need to apply a correction to the RVD if areas are in use
            RouteValueDictionary correctedValues = values;
            RouteCollection filteredRoutes = FilterRouteCollectionByArea(routes, targetArea, out usingAreas);
            if (usingAreas)
            {
                correctedValues = new RouteValueDictionary(values);
                correctedValues.Remove("area");
            }

            VirtualPathData vpd = filteredRoutes.GetVirtualPath(requestContext, correctedValues);
            return vpd;
        }
    }
}
