using AutoMapper;
using Ecommerce.BLL.ViewModels;
using Ecommerce.DAL;

namespace Ecommerce.BLL 
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>();

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Title));
        }
    }
}