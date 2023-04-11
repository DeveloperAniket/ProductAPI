using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest; 
using ProductService.ProductAPI.Models.Dtos;
using System;

namespace src.Extension
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(
            this IServiceCollection services, IConfiguration configuration
        )
        {
            var url = configuration["ELKConfiguration:Uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url))
                    .PrettyJson()
                    .DefaultIndex(defaultIndex);

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<ProductDto>(p =>
                        p.Ignore(x => x.Price)//it makes the price equals to 0
                                              //.Ignore(x => x.Id)
                            .Ignore(x => x.ImageUrl));//it makes the quantity to 0
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<ProductDto>(x => x.AutoMap()));
        }
    }
}
