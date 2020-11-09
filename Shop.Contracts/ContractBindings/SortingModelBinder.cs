using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shop.Contracts.V1;
using Shop.Contracts.V1.Requests.Queries;

namespace Shop.Contracts.ContractBindings
{
    public class SortingModelBinder : IModelBinder
    {
        private static readonly Dictionary<string, SortingDirections> DirectionsDictionary =
                new Dictionary<string, SortingDirections>
                {
                    { "ASC", SortingDirections.Ascending },
                    { "DESC", SortingDirections.Descending }
                };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var value = valueProviderResult.FirstValue; // get the value as string

            var sortingValues = value?.Split(",");
            var model = new List<SortingQuery>();

            if (sortingValues != null)
            {
                foreach (var sortValue in sortingValues)
                {
                    var splitValue = sortValue.Split('+');

                    // if sorting is only name
                    if (splitValue.Length == 1)
                    {
                        model.Add(new SortingQuery
                        {
                            Name = splitValue[0]
                        });
                    }

                    // if sorting has both direction and name
                    if (splitValue.Length == 2)
                    {
                        // check if direction exits if not than default ascending
                        var valueExists =
                                DirectionsDictionary.TryGetValue(splitValue[1].ToUpper(), out var direction);
                        model.Add(new SortingQuery
                        {
                            Name = splitValue[0],
                            Direction = valueExists
                                                ? direction
                                                : SortingDirections.Ascending
                        });
                    }
                }
            }

            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}