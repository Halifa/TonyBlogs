using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TonyBlogs.Framework
{
    public class Bootstrapper
    {
        private static ContainerBuilder containerBuilder;

        static Bootstrapper()
        {
            containerBuilder = new ContainerBuilder();
        }

        public void Start()
        { 
            Type baseType = typeof(IDependency);

            // 获取所有相关类库的程序集
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            var registBuilder = containerBuilder.RegisterAssemblyTypes(assemblies);

            containerBuilder.RegisterAssemblyTypes(assemblies).Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsImplementedInterfaces().InstancePerLifetimeScope();//InstancePerLifetimeScope 保证对象生命周期基于请求

            Type singletonType = typeof(ISignleton);
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(type => singletonType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsImplementedInterfaces().SingleInstance();//InstancePerLifetimeScope 保证对象生命周期基于请求

            containerBuilder.RegisterAssemblyModules(assemblies);

            var container = containerBuilder.Build();
            ContainerManager.SetContainer(container);
        }
    }
}
