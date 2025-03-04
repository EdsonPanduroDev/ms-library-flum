using Library.Domain.Util;

namespace Library.Application.Queries.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
    }

    public class BookRequest : PaginationRequest
    {
        public int Id { get; set; }
    }
}
