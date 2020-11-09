using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Shop.Api.Domain;
using Shop.DataAccess.Entities;

namespace Shop.Api.Repositories
{
    public class InMemoryCachedProductRepository : IReadOnlyProductRepository
    {
        private const string MyModelCacheKey = "Product";

        private readonly IMemoryCache cache;
        private readonly MemoryCacheEntryOptions cacheOptions;
        private readonly IProductRepository productRepository;

        public InMemoryCachedProductRepository(IProductRepository productRepository, IMemoryCache cache)
        {
            this.productRepository = productRepository;
            this.cache = cache;

            // 60 second cache
            cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return cache.GetOrCreate(MyModelCacheKey,
                entry =>
                {
                    entry.SetOptions(cacheOptions);

                    return productRepository.GetAllAsync();
                });
        }

        public Task<List<Product>> GetAllAsync(
            GetAllProductsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter)
        {
            var key = GenerateKey(filter, pagination, sortingFilter);

            return cache.GetOrCreate(key,
                entry =>
                {
                    entry.SetOptions(cacheOptions);

                    return productRepository.GetAllAsync(filter, pagination, sortingFilter);
                });
        }

        public Task<Product> GetByIdAsync(object id)
        {
            var key = MyModelCacheKey + "-" + id;

            return cache.GetOrCreate(key,
                entry =>
                {
                    entry.SetOptions(cacheOptions);

                    return productRepository.GetByIdAsync(id);
                });
        }

        private string GenerateKey(
            GetAllProductsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter)
        {
            var keyBuilder = new StringBuilder(MyModelCacheKey);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                keyBuilder.Append(filter.Name);
            }

            if (filter.PriceFrom.HasValue)
            {
                keyBuilder.Append(filter.PriceFrom.Value);
            }

            if (filter.PriceTo.HasValue)
            {
                keyBuilder.Append(filter.PriceTo.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                keyBuilder.Append(filter.Description);
            }

            if (filter.RatingFrom.HasValue)
            {
                keyBuilder.Append(filter.RatingFrom.Value);
            }

            return keyBuilder.ToString();
        }
    }
}