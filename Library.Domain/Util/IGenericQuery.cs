using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Util
{
    interface IGenericQuery
    {
        Task<IEnumerable<T>> ExecuteDirectAsync<T>(string procedure);

        Task<IEnumerable<T>> ExecuteDirectAsync<T>(string procedure, DynamicParameters parameters);

        Task<dynamic> SearchAsync(string procedure, string parametersXml);

        Task<IEnumerable<dynamic>> SearchAsync(string procedure, string parametersXml, Pagination pagination);

        Task<IEnumerable<dynamic>> FindAllAsync(string procedure, string parametersXml, Pagination pagination);

        Task<IEnumerable<T>> FindAllAsync<T>(string procedure, string parametersXml, Pagination pagination);
    }
}
