using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Helpers.Business;
using Core.Helpers.Interceptors;
using Core.Helpers.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dependency.Autofac
{
	public class AutofacBusinessModule:Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
			builder.RegisterType<UserManager>().As<IUserService>().InstancePerDependency();

			builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerDependency();
			builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

			builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();
			builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance()
				;
			builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();
			builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();

			builder.RegisterType<EfServiceDal>().As<IServiceDal>().SingleInstance();
			builder.RegisterType<ServiceManager>().As<IServiceService>().SingleInstance();

			builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
			builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();

			builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
			builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();

			builder.RegisterType<EfContactDal>().As<IContactDal>().SingleInstance();
			builder.RegisterType<ContactManager>().As<IContactService>().SingleInstance();

			builder.RegisterType<EfCountryDal>().As<ICountryDal>().SingleInstance();
			builder.RegisterType<CountryManager>().As<ICountryService>().SingleInstance();

			builder.RegisterType<EfPaymentMethodDal>().As<IPaymentMethodDal>().SingleInstance();
			builder.RegisterType<PaymentMethodManager>().As<IPaymentMethodService>().SingleInstance();

			builder.RegisterType<EfSocialNetworkDal>().As<ISocialNetworkDal>().SingleInstance();
			builder.RegisterType<SocialNetworkManager>().As<ISocialNetworkService>().SingleInstance();

			builder.RegisterType<AppDbContext>().As<AppDbContext>().SingleInstance();
			builder.RegisterType<AddPhotoHelper>().As<IAddPhotoHelperService>().SingleInstance();
			var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
				.EnableInterfaceInterceptors(new ProxyGenerationOptions()
				{
					Selector = new AspectInterceptorSelector()
				}).SingleInstance();
		}
	}
}
