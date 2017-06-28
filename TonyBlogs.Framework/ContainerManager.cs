using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Core.Lifetime;

namespace TonyBlogs.Framework
{
    public class ContainerManager
    {
     
         private static IContainer _container;
 
         public static void SetContainer(IContainer container)
         {
             _container = container;
         }
 
         public static IContainer Container
         {
             get { return _container; }
         }

         public static T Resolve<T>()
         {
             return _container.Resolve<T>();
         }

        public static object Resolve(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.Resolve(type);
        }

        public static bool IsRegistered(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.IsRegistered(serviceType);
        }

        public static object ResolveOptional(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.ResolveOptional(serviceType);
        }

        public static ILifetimeScope Scope()
        {
            return Container.BeginLifetimeScope();
        }
         
     }
    
}
