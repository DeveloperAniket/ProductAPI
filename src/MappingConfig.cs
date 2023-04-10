using AutoMapper;
using ProductService.ProductAPI.Models;
using ProductService.ProductAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, ProductEntity>();
                config.CreateMap<ProductEntity, ProductDto>();
            });

            return mappingConfig;
        }
    }
}
