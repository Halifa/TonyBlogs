using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TonyBlogs.WebApp
{
    public class MvcControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var controllerAssemblies = typeof(MvcApplication).Assembly;
            builder.RegisterAssemblyTypes(controllerAssemblies)
                .Where(t => typeof(IController).IsAssignableFrom(t) &&
                    t.Name.EndsWith("Controller", StringComparison.Ordinal));
        }
    }
}