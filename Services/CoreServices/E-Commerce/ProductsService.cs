using AutoMapper;
using Domain.Contracts;
using Domain.Entities.e_Commerce;
using Service.Exception_Implementation.ArgumantNullException;
using Service.Specification_Implementation.E_CommerceSpecifications;
using ServiceAbstraction.CoreServicesAbstractions.E_Commerce_Abstraction;
using SharedData.DTOs.E_CommerceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices.E_Commerce
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProductsService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<List<ProductBreifDTO>> FilterProductsAsync(
            int? pageNumber,
            int? itemPerPage,
            decimal? maxPrice = null,
            int? categoryId = null)
        {
            var spec = new FilterProductSpecification( pageNumber,
             itemPerPage, maxPrice, categoryId);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            if (products == null || !products.Any())
            {
                return new List<ProductBreifDTO>();
            }
            var productsDto = mapper.Map<List<ProductBreifDTO>>(products);
            return productsDto;
        }

        public async Task<CartItemDto> GetProductByIdAsync(int productId)
        {
            if(productId <= 0)
            {
                throw new ProductArgumentException("Product ID must be greater than zero.");
            }
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(productId);
            if (Product == null)
            {
                throw new ProductArgumentException($"Product with ID {productId} not found.");
            }
            var productDto = mapper.Map<CartItemDto>(Product);
            return productDto;
        }
    }
}
