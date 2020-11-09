﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Domain;
using Shop.Api.Extensions;
using Shop.Api.Helpers;
using Shop.DataAccess;
using Shop.DataAccess.Entities;

namespace Shop.Api.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetAllAsync(
            GetAllProductsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter);
    }

    public class ProductRepository : GenericRepository<Product, DataContext>, IProductRepository
    {
        private readonly ISortHelper<Product> sortHelper;

        public ProductRepository(DataContext context, ISortHelper<Product> sortHelper)
                : base(context)
        {
            this.sortHelper = sortHelper;
        }

        public Task<List<Product>> GetAllAsync(
            GetAllProductsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter)
        {
            IQueryable<Product> queryable = Context.Products;

            queryable = FilterProducts(queryable, filter);
            queryable = sortHelper.ApplySort(queryable, sortingFilter);

            return queryable.ApplyPagination(pagination).ToListAsync();
        }

        private IQueryable<Product> FilterProducts(IQueryable<Product> queryable, GetAllProductsFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.PriceFrom.HasValue)
            {
                queryable = queryable.Where(x => x.Price >= filter.PriceFrom.Value);
            }

            if (filter.PriceTo.HasValue)
            {
                queryable = queryable.Where(x => x.Price <= filter.PriceTo.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                queryable = queryable.Where(x =>
                        x.Description.ToLower().Contains(filter.Description.ToLower()));
            }

            if (filter.RatingFrom.HasValue)
            {
                queryable = queryable.Where(x => x.Ratings.Sum(x => x.Value) >= filter.RatingFrom.Value);
            }

            return queryable;
        }
    }
}