using AutoMapper;
using FluentValidation;
using Ecommerce.BLL.ViewModels;
using Ecommerce.Common;
using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddToCartDTO> _addValidator;
        private readonly IValidator<UpdateCartDTO> _updateValidator;
        private readonly IErrorMapper _errorMapper;

        public CartManager(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<AddToCartDTO> addValidator,
            IValidator<UpdateCartDTO> updateValidator,
            IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
            _errorMapper = errorMapper;
        }

        public async Task<GeneralResult<CartDTO>> GetUserCartAsync(string userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);

            if (cart == null)
                return GeneralResult<CartDTO>.NotFound();

            var result = _mapper.Map<CartDTO>(cart);
            return GeneralResult<CartDTO>.SuccessResult(result);
        }

        public async Task<GeneralResult<bool>> AddToCartAsync(string userId, AddToCartDTO dto)
        {
            var validationResult = await _addValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);
                return GeneralResult<bool>.FailResult(errors);
            }

            var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId, Items = new List<CartItem>() };
                _unitOfWork.CartRepository.Add(cart);
            }

            var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (item == null)
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }
            else
            {
                item.Quantity += dto.Quantity;
            }

            await _unitOfWork.SaveChangesAsync();
            return GeneralResult<bool>.SuccessResult(true);
        }

        public async Task<GeneralResult<bool>> UpdateCartAsync(string userId, UpdateCartDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);
                return GeneralResult<bool>.FailResult(errors);
            }

            var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);

            if (cart == null)
                return GeneralResult<bool>.NotFound();

            var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (item == null)
                return GeneralResult<bool>.FailResult("Item not found");

            item.Quantity = dto.Quantity;

            await _unitOfWork.SaveChangesAsync();
            return GeneralResult<bool>.SuccessResult(true);
        }

        public async Task<GeneralResult<bool>> RemoveFromCartAsync(string userId, int productId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);

            if (cart == null)
                return GeneralResult<bool>.NotFound();

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
                cart.Items.Remove(item);

            await _unitOfWork.SaveChangesAsync();
            return GeneralResult<bool>.SuccessResult(true);
        }
    }
}