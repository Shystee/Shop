﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shop.Api.Domain;
using Shop.Contracts.V1;
using Shop.Contracts.V1.Requests;
using Shop.Contracts.V1.Requests.Queries;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Services
{
    public interface IPaginationService
    {
        public PagedResponse<ProductResponse> CreateProductPaginatedResponse(
            PaginationFilter pagination,
            SortingFilter sortingFilter,
            List<ProductResponse> response);

        public PagedResponse<RatingResponse> CreateProductRatingsPaginatedResponse(
            int productId,
            PaginationFilter pagination,
            SortingFilter sortingFilter,
            List<RatingResponse> response);
    }

    public class PaginationService : IPaginationService
    {
        private static readonly Dictionary<SortingDirections, string> DirectionsDictionary =
                new Dictionary<SortingDirections, string>
                {
                    { SortingDirections.Ascending, "ASC" },
                    { SortingDirections.Descending, "DESC" }
                };

        private readonly IUriService uriService;

        public PaginationService(IUriService uriService)
        {
            this.uriService = uriService;
        }

        public PagedResponse<ProductResponse> CreateProductPaginatedResponse(
            PaginationFilter pagination,
            SortingFilter sortingFilter,
            List<ProductResponse> response)
        {
            var nextPage = pagination.PageNumber >= 1
                                   ? uriService
                                     .GetAllProductsUri(new PaginationQuery(pagination.PageNumber + 1,
                                         pagination.PageSize))
                                     .ToString()
                                   : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                                       ? uriService
                                         .GetAllProductsUri(new PaginationQuery(pagination.PageNumber - 1,
                                             pagination.PageSize))
                                         .ToString()
                                       : null;

            return CreatePaginatedResponse(pagination, sortingFilter, response, nextPage, previousPage);
        }

        public PagedResponse<RatingResponse> CreateProductRatingsPaginatedResponse(
            int productId,
            PaginationFilter pagination,
            SortingFilter sortingFilter,
            List<RatingResponse> response)
        {
            var nextPage = pagination.PageNumber >= 1
                                   ? uriService.GetProductRatingUri(productId,
                                                   new PaginationQuery(pagination.PageNumber + 1,
                                                       pagination.PageSize))
                                               .ToString()
                                   : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                                       ? uriService.GetProductRatingUri(productId,
                                                       new PaginationQuery(pagination.PageNumber - 1,
                                                           pagination.PageSize))
                                                   .ToString()
                                       : null;

            return CreatePaginatedResponse(pagination, sortingFilter, response, nextPage, previousPage);
        }

        private static PagedResponse<T> CreatePaginatedResponse<T>(
            PaginationFilter pagination,
            SortingFilter sortingFilter,
            List<T> response,
            string nextPage,
            string previousPage)
        {
            return new PagedResponse<T>
            {
                Data = response,
                Metadata = new Metadata
                {
                    Pagination = new Pagination
                    {
                        PageNumber = pagination.PageNumber >= 1
                                             ? pagination.PageNumber
                                             : (int?)null,
                        PageSize = pagination.PageSize >= 1
                                           ? pagination.PageSize
                                           : (int?)null,
                        NextPage = response.Count > 0
                                           ? nextPage
                                           : null,
                        PreviousPage = previousPage
                    },
                    SortedBy = SortedBy<T>(sortingFilter)
                }
            };
        }

        private static List<Sorting> SortedBy<T>(SortingFilter sortingFilter)
        {
            if (sortingFilter == null) return null;

            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var sorts = new List<Sorting>();

            foreach (var sort in sortingFilter.Sortings)
            {
                var property = propertyInfos.FirstOrDefault(pi =>
                        pi.Name.Equals(sort.Name, StringComparison.InvariantCultureIgnoreCase));

                if (property == null) continue;

                sorts.Add(new Sorting
                {
                    Field = sort.Name,
                    Order = DirectionsDictionary[sort.Direction]
                });
            }

            return sorts;
        }
    }
}