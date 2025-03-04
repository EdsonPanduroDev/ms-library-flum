using Library.Application.Queries.ViewModels;

namespace Library.Application.Queries.Interface
{
    public interface IBookCopyQuery
    {
        Task<IEnumerable<dynamic>> GetByCopyId(BookRequest request);
    }
}
