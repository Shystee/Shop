﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Domain;
using Shop.Api.Extensions;
using Shop.DataAccess;
using Shop.DataAccess.Entities;

namespace Shop.Api.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        bool DoesProductExist(int productId);

        Task<List<Product>> GetAllAsync(
            GetAllProductsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter);
    }

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context)
                : base(context)
        {
        }

        public bool DoesProductExist(int productId)
        {
            return Context.Products.Any(x => x.Id == productId);
        }

        public Task<List<Product>> GetAllAsync(
            GetAllProductsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter)
        {
            IQueryable<Product> queryable = Context.Products.Include(x => x.Ratings);

            queryable = FilterProducts(queryable, filter);
            queryable = ApplySort(queryable, sortingFilter);

            return queryable.ApplyPagination(pagination).ToListAsync();
        }

        public override Task<Product> GetByIdAsync(object id)
        {
            return Context.Products.Include(x => x.Ratings).FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        private static IQueryable<Product> ApplySort(IQueryable<Product> queryable, SortingFilter filter)
        {
            foreach (var sorting in filter.Sortings)
            {
                switch (sorting.Name)
                {
                    case "description":
                        queryable = queryable.OrderBy(x => x.Description, sorting.Direction);

                        break;
                    case "name":
                        queryable = queryable.OrderBy(x => x.Name, sorting.Direction);

                        break;
                    case "price":
                        queryable = queryable.OrderBy(x => x.Price, sorting.Direction);

                        break;
                    case "averageRating":
                        queryable = queryable.OrderBy(x => x.Ratings.Sum(r => r.Value), sorting.Direction);

                        break;
                }
            }

            return queryable;
        }

        private static IQueryable<Product> FilterProducts(IQueryable<Product> queryable, GetAllProductsFilter filter)
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