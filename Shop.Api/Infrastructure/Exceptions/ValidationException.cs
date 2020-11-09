using System;
using System.Collections.Generic;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Infrastructure.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(ErrorModel error)
        {
            Errors.Add(error);
        }

        public ValidationException(IEnumerable<ErrorModel> errors)
        {
            Errors.AddRange(errors);
        }

        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}