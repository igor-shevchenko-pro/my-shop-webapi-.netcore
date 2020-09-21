using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.BL.Services;
using MyShop.BL.Services.Auth;
using MyShop.BL.Services.UserAccount;
using MyShop.Core.Interfaces.Managers;
using MyShop.Core.Interfaces.Managers.Base;
using MyShop.Core.Interfaces.Managers.LetterManagers.Base;
using MyShop.Core.Interfaces.Managers.LetterManagers.Email;
using MyShop.Core.Interfaces.Managers.LetterManagers.Sms;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Auth;
using MyShop.Core.Interfaces.Repositories.ManyToMany;
using MyShop.Core.Interfaces.Repositories.UserAccount;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Interfaces.Services.Auth;
using MyShop.Core.Interfaces.Services.UserAccount;
using MyShop.Core.Managers;
using MyShop.Core.Managers.Base;
using MyShop.Core.Managers.Base.LetterManagers;
using MyShop.Core.Managers.Base.LetterManagers.Email;
using MyShop.Core.Managers.LetterManagers.Base;
using MyShop.Core.Managers.LetterManagers.Sms;
using MyShop.DL.Repositories;
using MyShop.DL.Repositories.Auth;
using MyShop.DL.Repositories.ManyToMany;
using MyShop.DL.Repositories.UserAccount;

namespace MyShop.BL.DIConfigurations
{
    public abstract class DIRegistration
    {
        protected IConfiguration Configuration;

        public DIRegistration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public abstract void RegisterConfigs(ref IServiceCollection services);

        public virtual void RegisterAll(ref IServiceCollection services)
        {
            RegisterConfigs(ref services);
            RegisterServices(ref services);
            RegisterRepositories(ref services);
            RegisterManagers(ref services);
            RegisterCache(ref services);
        }

        public virtual void RegisterServices(ref IServiceCollection services)
        {
            // Services
            services.AddTransient<IGenderService, GenderService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IFileEntityService, FileEntityService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVerificationTokenService, VerificationTokenService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IImageGalleryService, ImageGalleryService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IAccountService, AccountService>();
        }

        public virtual void RegisterRepositories(ref IServiceCollection services)
        {
            // Repositories
            services.AddTransient<IFileEntityRepository, FileEntityRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IVerificationTokenRepository, VerificationTokenRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IModificationRepository, ModificationRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IImageGalleryRepository, ImageGalleryRepository>();
            // ManyToMany Repositories
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            services.AddTransient<ICategoryProductRepository, CategoryProductRepository>();
            services.AddTransient<IOrderProductRepository, OrderProductRepository>();
            services.AddTransient<ISupplierProductRepository, SupplierProductRepository>();
        }

        public virtual void RegisterManagers(ref IServiceCollection services)
        {
            // Managers
            services.AddTransient<IPasswordManager, PasswordManager>();
            services.AddTransient<IFileEntityManager, FileEntityManager>();
            services.AddTransient<IFileEntityExtensionManager, FileEntityExtensionManager>();
            services.AddTransient<ISmsSenderManager, SmsSenderManager>();
            services.AddTransient<ISmsContentBuilderManager, SmsContentBuilderManager>();
            services.AddTransient<ISmsTypeBuilderManager, SmsTypeBuilderManager>();
            services.AddTransient<IEmailSenderManager, EmailSenderManager>();
            services.AddTransient<IEmailContentBuilderManager, EmailContentBuilderManager>();
            services.AddTransient<IEmailTypeBuilderManager, EmailTypeBuilderManager>();
            services.AddTransient<ITextLetterBuilderManager, TextLetterBuilderManager>();
        }

        public virtual void RegisterCache(ref IServiceCollection services)
        {
            // Cache
            //services.AddSingleton<ICacheCityManager>(new CacheCityManager(60));
        }
    }
}
