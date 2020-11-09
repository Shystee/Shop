using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Domain;
using Shop.Contracts.V1;

namespace Shop.Api.Helpers
{
    public interface ISortHelper<T>
    {
        public IQueryable<T> ApplySort(IQueryable<T> entities, SortingFilter filter);
    }

    public class SortHelper<T> : ISortHelper<T>
    {
        public IQueryable<T> ApplySort(IQueryable<T> entities, SortingFilter filter)
        {
            if (!entities.Any())
            {
                return entities;
            }

            if (!filter.Sortings.Any())
            {
                return entities;
            }

            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sort in filter.Sortings)
            {
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                        pi.Name.Equals(sort.Name, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                {
                    continue;
                }

                // orderedItems = filteredItems.OrderBy(item => typeof(area).GetProperty(colName).GetValue(item).ToString());
                // p =>EF.Property<object>(p, objectProperty.Name)
                entities = sort.Direction == SortingDirections.Ascending
                                   ? entities.OrderBy(p => EF.Property<object>(p, objectProperty.Name))
                                   : entities.OrderByDescending(p =>
                                           EF.Property<object>(p, objectProperty.Name));
            }

            return entities;
        }
    }
}