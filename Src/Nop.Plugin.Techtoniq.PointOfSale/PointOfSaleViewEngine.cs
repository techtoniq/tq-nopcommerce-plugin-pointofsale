using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Techtoniq.PointOfSale
{
    public class PointOfSaleViewEngine : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var exViewLocations = new string[]
                { 
                    "/Plugins/Techtoniq.PointOfSale/Views/{1}/{0}.cshtml",
                    "/Plugins/Techtoniq.PointOfSale/Views/Shared/{0}.cshtml",
                }.Concat(viewLocations);

            return exViewLocations;
        }
    }
}
