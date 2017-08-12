using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Compilation;

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
            Assembly[] assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsImplementedInterfaces().InstancePerDependency();//每次解析获得新实例

            Type singletonType = typeof(ISignleton);
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(type => singletonType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsImplementedInterfaces().SingleInstance();// 保证对象生命周期基于单例

            containerBuilder.RegisterAssemblyModules(assemblies);//所有继承module中的类都会被注册

            var container = containerBuilder.Build();
            ContainerManager.SetContainer(container);
        }
    }
}
