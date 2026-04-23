using AutoMapper;
using Ecommerce.BLL.ViewModels;
using Ecommerce.DAL;

namespace Ecommerce.BLL 
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDTO>();

            CreateMap<Category, CategoryDetailsDTO>();

            CreateMap<CategoryFormDTO, Category>();

            CreateMap<Category, CategoryFormDTO>();
        }
    }
}