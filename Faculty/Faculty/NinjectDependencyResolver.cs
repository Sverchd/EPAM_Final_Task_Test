using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogicLayer;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Context;
using DataAccessLayer.Managers;
using DataAccessLayer.Repositories;
using Ninject;

namespace Faculty
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ICourseRepository>().To<CourseRepository>();
            kernel.Bind<ICourseService>().To<CourseService>();
            kernel.Bind<FacultyDbContext>().ToSelf().WithConstructorArgument("FacultyContext");

            

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<IThemeRepository>().To<ThemeRepository>();
            kernel.Bind<IThemeService>().To<ThemeService>();
        }
    }
}