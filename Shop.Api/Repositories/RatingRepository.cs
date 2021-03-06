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
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<List<Rating>> GetAllAsync(
            GetAllRatingsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter);
    }

    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(DataContext context)
                : base(context)
        {
        }

        public Task<List<Rating>> GetAllAsync(
            GetAllRatingsFilter filter,
            PaginationFilter pagination,
            SortingFilter sortingFilter)
        {
            IQueryable<Rating> queryable = Context.Ratings;

            queryable = FilterProducts(queryable, filter);
            queryable = ApplySort(queryable, sortingFilter);

            return queryable.ApplyPagination(pagination).ToListAsync();
        }

        private IQueryable<Rating> ApplySort(IQueryable<Rating> queryable, SortingFilter filter)
        {
            foreach (var sorting in filter.Sortings)
            {
                switch (sorting.Name)
                {
                    case "comment":
                        queryable = queryable.OrderBy(x => x.Comment, sorting.Direction);

                        break;
                    case "value":
                        queryable = queryable.OrderBy(x => x.Value, sorting.Direction);

                        break;
                }
            }

            return queryable;
        }

        private IQueryable<Rating> FilterProducts(IQueryable<Rating> queryable, GetAllRatingsFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Comment))
            {
                queryable = queryable.Where(x => x.Comment.ToLower().Contains(filter.Comment.ToLower()));
            }

            if (filter.RatingFrom.HasValue)
            {
                queryable = queryable.Where(x => x.Value >= filter.RatingFrom.Value);
            }

            if (filter.RatingTo.HasValue)
            {
                queryable = queryable.Where(x => x.Value <= filter.RatingTo.Value);
            }

            return queryable;
        }
    }
}