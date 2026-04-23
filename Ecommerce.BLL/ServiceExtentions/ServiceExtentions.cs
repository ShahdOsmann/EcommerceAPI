using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Ecommerce.DAL;
 
namespace Ecommerce.BLL
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services)
        {
             services.AddScoped<IProductManager, ProductManager>();
             services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddValidatorsFromAssembly(typeof(ServiceExtentions).Assembly);
            services.AddAutoMapper(typeof(ServiceExtentions).Assembly);
            services.AddScoped<IErrorMapper, ErrorMapper>();
            services.AddScoped<IImageManager, ImageManager>(); 
            services.AddScoped<IValidator<ImageUploadDto>, ImageUploadDtoValidator>();
             services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            return services;
        }
    }
}