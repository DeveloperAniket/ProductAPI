using ProductService.ProductAPI.Models; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace ProductService.ProductAPI.DbContexts
{
    public class RDSContext : DbContext
    { 

        public RDSContext(DbContextOptions<RDSContext> options) : base(options) { }


        public virtual DbSet<ProductEntity> Product { get; set; }

 
    }
}
