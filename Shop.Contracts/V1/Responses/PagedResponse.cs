using System.Collections.Generic;

namespace Shop.Contracts.V1.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse()
        {
        }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }

        public Metadata Metadata { get; set; }
    }
}