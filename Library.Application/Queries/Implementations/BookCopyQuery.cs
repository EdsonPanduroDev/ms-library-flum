using Library.Application.Generic;
using Library.Application.Queries.Interface;
using Library.Application.Queries.ViewModels;
using Library.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.Implementations
{
    public class BookCopyQuery : IBookCopyQuery
    {
        private readonly IGenericQuery _genericQuery;

        public BookCopyQuery(IGenericQuery genericQuery)
        {
            _genericQuery = genericQuery;
        }
        public async Task<IEnumerable<dynamic>> GetByCopyId(BookRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"book_copy_id", request.Id}
            };

            var xml = ConvertTo.Xml(parameters);

            var result = await _genericQuery.SearchAsync(@"dbo.ADV_BOOK_SEARCH", ConvertTo.Xml(parameters), request.pagination);

            return result;
        }
    }
}
