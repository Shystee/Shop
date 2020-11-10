using System.Collections.Generic;
using Shop.Api.Constants;
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
            GetAllProductsFilter filter,
            SortingFilter sorting,
            List<ProductResponse> response);

        public PagedResponse<RatingResponse> CreateProductRatingsPaginatedResponse(
            int productId,
            PaginationFilter pagination,
            GetAllRatingsFilter filter,
            SortingFilter sorting,
            List<RatingResponse> response);
    }

    public class PaginationService : IPaginationService
    {
        private readonly IUriService uriService;

        public PaginationService(IUriService uriService)
        {
            this.uriService = uriService;
        }

        public PagedResponse<ProductResponse> CreateProductPaginatedResponse(
            PaginationFilter pagination,
            GetAllProductsFilter filter,
            SortingFilter sorting,
            List<ProductResponse> response)
        {
            var nextPage = pagination.PageNumber >= 1
                    ? uriService
                      .GetAllProductsUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize),
                          filter,
                          sorting)
                      .ToString()
                    : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                    ? uriService
                      .GetAllProductsUri(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize),
                          filter,
                          sorting)
                      .ToString()
                    : null;

            return CreatePaginatedResponse(pagination, sorting, response, nextPage, previousPage);
        }

        public PagedResponse<RatingResponse> CreateProductRatingsPaginatedResponse(
            int productId,
            PaginationFilter pagination,
            GetAllRatingsFilter filter,
            SortingFilter sorting,
            List<RatingResponse> response)
        {
            var nextPage = pagination.PageNumber >= 1
                    ? uriService.GetProductRatingUri(productId,
                                    new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize),
                                    filter,
                                    sorting)
                                .ToString()
                    : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                    ? uriService.GetProductRatingUri(productId,
                                    new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize),
                                    filter,
                                    sorting)
                                .ToString()
                    : null;

            return CreatePaginatedResponse(pagination, sorting, response, nextPage, previousPage);
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
                    SortedBy = SortedBy(sortingFilter)
                }
            };
        }

        private static List<Sorting> SortedBy(SortingFilter sortingFilter)
        {
            if (sortingFilter == null) return null;

            var sorts = new List<Sorting>();

            foreach (var sort in sortingFilter.Sortings)
            {
                sorts.Add(new Sorting
                {
                    Field = sort.Name,
                    Order = SortingConstants.DirectionsDictionary[sort.Direction]
                });
            }

            return sorts;
        }
    }
}