using Web365Business.Front_End.IRepository;
using Web365Business.Front_End.Repository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.Repository;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web365.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Web365.App_Start.NinjectWebCommon), "Stop")]

namespace Web365.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILayoutContentDAFERepository>().To<LayoutContentDAFERepository>();
            kernel.Bind<IAdvertiesDAFERepository>().To<AdvertiesDAFERepository>();
            kernel.Bind<IArticleTypeRepositoryFE>().To<ArticleTypeRepositoryFE>();
            kernel.Bind<IPictureRepositoryFE>().To<PictureRepositoryFE>();
            kernel.Bind<IArticleRepositoryFE>().To<ArticleRepositoryFE>();
            kernel.Bind<IProductRepositoryFE>().To<ProductRepositoryFE>();
            kernel.Bind<IProductTypeRepositoryFE>().To<ProductTypeRepositoryFE>();
            kernel.Bind<IOtherDAFERepository>().To<OtherDAFERepository>();
            kernel.Bind<IOrderDAFERepository>().To<OrderDAFERepository>();
            kernel.Bind<IVideoDAFERepository>().To<VideoDAFERepository>();

            kernel.Bind<ILayoutContentRepositoryFE>().To<LayoutContentRepositoryFE>();
            kernel.Bind<IAdvertiesRepositoryFE>().To<AdvertiesRepositoryFE>();
            kernel.Bind<IArticleTypeDAFERepository>().To<ArticleTypeDAFERepository>();
            kernel.Bind<IPictureDAFERepository>().To<PictureDAFERepository>();
            kernel.Bind<IArticleDAFERepository>().To<ArticleDAFERepository>();
            kernel.Bind<IProductDAFERepository>().To<ProductDAFERepository>();
            kernel.Bind<IProductTypeDAFERepository>().To<ProductTypeDAFERepository>();
            kernel.Bind<IOtherRepositoryFE>().To<OtherRepositoryFE>();
            kernel.Bind<IOrderRepositoryFE>().To<OrderRepositoryFE>();
            kernel.Bind<IVideoRepositoryFE>().To<VideoRepositoryFE>();
        }        
    }
}
