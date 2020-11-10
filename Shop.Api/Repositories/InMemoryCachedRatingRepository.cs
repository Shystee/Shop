using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Shop.Api.Domain;
using Shop.Api.Extensions;
using Shop.DataAccess.Entities;

namespace Shop.Api.Repositories
{
    public class InMemoryCachedRatingRepository : IRatingRepository
    {
        private const string MyModelCacheKey = "Product";

        private readonly IMemoryCache cache;
        private readonly MemoryCacheEntryOptions cacheOptions;
        private readonly IRatingRepository ratingRepository;

        public InMemoryCachedRatingRepository(IRatingRepository ratingRepository, IMemoryCache cache)
        {
            this.ratingRepository = ratingRepository;
            this.cache = cache;

            // 60 second cache
            cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
        }

        public Task AddAsync(Rating model)
        {
            return ratingRepository.AddAsync(model);
        }

        public Task<IEnumerable<Rating>> GetAllAsync()
        {
            return cache.GetOrCreate(MyModelCacheKey,
                entry =>
                {
                    entry.SetOptions(cacheOptions);

                    return ratingRepository.GetAllAsync();
                });
        }

        public Task<List<Rating>> GetAllAsync(
            GetAllRatingsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter)
        {
            var key = GenerateKey(filter, pagination, sortingFilter);

            return cache.GetOrCreate(key,
                entry =>
                {
                    entry.SetOptions(cacheOptions);

                    return ratingRepository.GetAllAsync(filter, pagination, sortingFilter);
                });
        }

        public Task<Rating> GetByIdAsync(object id)
        {
            var key = MyModelCacheKey + "-" + id;

            return cache.GetOrCreate(key,
                entry =>
                {
                    entry.SetOptions(cacheOptions);

                    return ratingRepository.GetByIdAsync(id);
                });
        }

        public bool HasChanges()
        {
            return ratingRepository.HasChanges();
        }

        public void Remove(Rating model)
        {
            var key = MyModelCacheKey + "-" + model.Id;
            ratingRepository.Remove(model);
            cache.Remove(key);
        }

        public Task<bool> SaveAsync()
        {
            return ratingRepository.SaveAsync();
        }

        public void Update(Rating model)
        {
            var key = MyModelCacheKey + "-" + model.Id;
            ratingRepository.Update(model);
            cache.Set(key, model, cacheOptions);
        }

        private string GenerateKey(
            GetAllRatingsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter)
        {
            return MyModelCacheKey.GeneratePaginationQuery(pagination)
                                  .GenerateRatingFilterQuery(filter)
                                  .GenerateSortingQuery(sortingFilter);
        }
    }
}