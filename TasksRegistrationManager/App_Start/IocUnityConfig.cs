using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Connection;
using Microsoft.Practices.Unity;
using TasksRegistrationManager.Controllers;
using TasksRegistrationManager.Infrastructure;

namespace TasksRegistrationManager.App_Start
{
    public static class IocUnityConfig
    {
        public static void ConfigureUnityContainer()
        {
            IUnityContainer unityContainer = new UnityContainer();

            DiBindings(unityContainer);

            DependencyResolver.SetResolver(new UnityDependencyResolver(unityContainer));
        }

        private static void DiBindings(IUnityContainer unity)
        {
            unity.RegisterType<PersonController>(new InjectionConstructor(new DbConnectionManager()));
            unity.RegisterType<TaskController>(new InjectionConstructor(new DbConnectionManager()));
            //unity.RegisterType<HomeController>(new InjectionConstructor(new UserAuthModel(), new UserAuthManager(), new BPMonlineServiceManager()));
        }
    }
}