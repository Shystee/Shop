﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Shop.Api.Domain;
using Shop.Api.Extensions;
using Shop.DataAccess.Entities;

namespace Shop.Api.Repositories
{
    public class InMemoryCachedProductRepository : IProductRepository
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

        public Task AddAsync(Product model)
        {
            return productRepository.AddAsync(model);
        }

        public bool DoesProductExist(int productId)
        {
            var key = MyModelCacheKey + "-" + productId;

            return cache.TryGetValue(key, out _);
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

        public bool HasChanges()
        {
            return productRepository.HasChanges();
        }

        public void Remove(Product model)
        {
            var key = MyModelCacheKey + "-" + model.Id;
            productRepository.Remove(model);
            cache.Remove(key);
        }

        public Task<bool> SaveAsync()
        {
            return productRepository.SaveAsync();
        }

        public void Update(Product model)
        {
            var key = MyModelCacheKey + "-" + model.Id;
            productRepository.Update(model);
            cache.Set(key, model, cacheOptions);
        }

        private string GenerateKey(
            GetAllProductsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter)
        {
            return MyModelCacheKey.GeneratePaginationQuery(pagination)
                                  .GenerateProductFilterQuery(filter)
                                  .GenerateSortingQuery(sortingFilter);
        }
    }
}