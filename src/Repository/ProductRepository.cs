using AutoMapper;
using ProductService.ProductAPI;
using ProductService.ProductAPI.Models;
using ProductService.ProductAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.ProductAPI.DbContexts;

namespace ProductService.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly RDSContext _db;
        private IMapper _mapper;

        public ProductRepository(RDSContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            ProductEntity product = _mapper.Map<ProductDto, ProductEntity>(productDto);
            if (product.Id > 0)
            {
                _db.Product.Update(product);
            }
            else
            {
                _db.Product.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductEntity, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                ProductEntity product = await _db.Product.FirstOrDefaultAsync(u => u.Id == productId);
                if (product== null)
                {
                    return false;
                }
                _db.Product.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _db.Product.Where(x => x.Id == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<ProductEntity> productList = await _db.Product.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);

        }
    }
}
