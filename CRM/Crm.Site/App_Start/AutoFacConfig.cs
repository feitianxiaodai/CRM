using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.Site.App_Start
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// 利用autofac 来创建仓储层和业务逻辑层的接口的具体实例
    /// </summary>
    public class AutoFacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            //autofac代替默认的控制器工厂来实例化对象
            Assembly ass = Assembly.Load("Crm.Site");
            builder.RegisterControllers(ass);

            //通过查找所有Respository类实例化对象然后将实例化后的类自动赋值给相应的接口
            builder.RegisterTypes(Assembly.Load("Crm.Repository").GetTypes()).AsImplementedInterfaces();

            //查找Services 逻辑层的类赋值给父接口变量
            builder.RegisterTypes(Assembly.Load("Crm.Services").GetTypes()).AsImplementedInterfaces();

            //mvc控制器的创建过程给autofac.dll
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}