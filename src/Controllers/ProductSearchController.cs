using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using ProductService.ProductAPI.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSearchController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<ProductSearchController> _logger;

        public ProductSearchController(IElasticClient elasticClient, ILogger<ProductSearchController> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }
 

        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> Get(string keyword)
        {
            var results = await _elasticClient.SearchAsync<ProductDto>(
                s => s.Query(
                    q => q.QueryString(
                        d => d.Query('*' + keyword + '*')
                    )
                ).Size(1000)
            );

            return Ok(results.Documents.ToList());
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<IActionResult> Post(ProductDto product)
        {
            await _elasticClient.IndexDocumentAsync(product);
            return Ok();
        }
    }
}
