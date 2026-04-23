using AutoMapper;
using FluentValidation;
using Ecommerce.BLL.ViewModels;
using Ecommerce.Common;
using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDTO> _validator;
        private readonly IErrorMapper _errorMapper;

        public OrderManager(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<CreateOrderDTO> validator,
            IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _errorMapper = errorMapper;
        }

        public async Task<GeneralResult<int>> CreateOrderAsync(string userId, CreateOrderDTO dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);
                return GeneralResult<int>.FailResult(errors);
            }

            var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);

            if (cart == null || !cart.Items.Any())
                return GeneralResult<int>.FailResult("Cart is empty");

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Product.Price
                }).ToList(),
                TotalPrice = cart.Items.Sum(i => i.Quantity * i.Product.Price)
            };

            _unitOfWork.OrderRepository.Add(order);

            _unitOfWork.CartRepository.Delete(cart);

            await _unitOfWork.SaveChangesAsync();

            return GeneralResult<int>.SuccessResult(order.Id);
        }

        public async Task<GeneralResult<IEnumerable<OrderDTO>>> GetUserOrdersAsync(string userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserId(userId);

            var result = _mapper.Map<IEnumerable<OrderDTO>>(orders);

            return GeneralResult<IEnumerable<OrderDTO>>.SuccessResult(result);
        }

        public async Task<GeneralResult<OrderDTO>> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderDetails(id);

            if (order == null)
                return GeneralResult<OrderDTO>.NotFound();

            var result = _mapper.Map<OrderDTO>(order);

            return GeneralResult<OrderDTO>.SuccessResult(result);
        }
    }
}