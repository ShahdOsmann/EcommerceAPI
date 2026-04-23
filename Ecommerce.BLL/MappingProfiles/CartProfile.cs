using AutoMapper;
using Ecommerce.BLL.ViewModels;
using Ecommerce.DAL;

namespace Ecommerce.BLL 
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDTO>();

            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Title))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Product.Price));
        }
    }
}