using Dapper;
using Library.Domain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Generic
{
    public class GenericQuery : IGenericQuery
    {
        private readonly string _connectionString;

        public GenericQuery(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException("connectionString");
        }

        public async Task<IEnumerable<T>> ExecuteDirectAsync<T>(string procedure)
        {
            IEnumerable<T> result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QueryAsync<T>(procedure, null, null, null, CommandType.StoredProcedure);
            }

            return result;
        }

        public async Task<IEnumerable<T>> ExecuteDirectAsync<T>(string procedure, DynamicParameters parameters)
        {
            IEnumerable<T> result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QueryAsync<T>(procedure, parameters, null, null, CommandType.StoredProcedure);
            }

            return result;
        }

        public async Task<dynamic> SearchAsync(string procedure, string parametersXml)
        {
            object result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@piv_orderBy", "", DbType.String);
                result = await connection.QueryFirstOrDefaultAsync(procedure, parameters, null, null, CommandType.StoredProcedure);
            }

            return result;
        }

        public async Task<IEnumerable<dynamic>> SearchAsync(string procedure, string parametersXml, Pagination pagination)
        {
            IEnumerable<object> result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@piv_orderBy", pagination.sort, DbType.String);
                result = await connection.QueryAsync(procedure, parameters, null, null, CommandType.StoredProcedure);
            }

            return result;
        }

        public async Task<IEnumerable<dynamic>> FindAllAsync(string procedure, string parametersXml, Pagination pagination)
        {
            IEnumerable<object> result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@pii_paginaActual", pagination.pageIndex, DbType.Int32);
                parameters.Add("@pii_cantidadMostrar", pagination.pageSize, DbType.Int32);
                parameters.Add("@piv_orderBy", pagination.sort, DbType.String);
                parameters.Add("@poi_totalRegistros", null, DbType.Int32, ParameterDirection.Output);
                result = await connection.QueryAsync(procedure, parameters, null, null, CommandType.StoredProcedure);
                pagination.total = parameters.Get<int>("@poi_totalRegistros");
            }

            return result;
        }

        public async Task<IEnumerable<T>> FindAllAsync<T>(string procedure, string parametersXml, Pagination pagination)
        {
            SetupMapper<T>();
            IEnumerable<T> result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@pii_paginaActual", pagination.pageIndex, DbType.Int32);
                parameters.Add("@pii_cantidadMostrar", pagination.pageSize, DbType.Int32);
                parameters.Add("@piv_orderBy", GetOrderByExpression<T>(pagination), DbType.String);
                parameters.Add("@poi_totalRegistros", null, DbType.Int32, ParameterDirection.Output);
                result = await connection.QueryAsync<T>(procedure, parameters, null, null, CommandType.StoredProcedure);
                pagination.total = parameters.Get<int>("@poi_totalRegistros");
            }

            return result;
        }

        private void SetupMapper<T>()
        {
            SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(typeof(T), (Type type, string columnName) => type.GetProperties().FirstOrDefault((PropertyInfo prop) => prop.GetCustomAttributes(inherit: false).OfType<ColumnAttribute>().Any((ColumnAttribute attr) => attr.Name == columnName))));
        }

        private string GetOrderByExpression<T>(Pagination pagination)
        {
            if (pagination.sort == null)
            {
                return string.Empty;
            }

            string columnAttribute = GetColumnAttribute<T>(pagination.sort);
            return (columnAttribute != null) ? (columnAttribute + " " + (pagination.sort ?? "ASC")) : string.Empty;
        }

        private string GetColumnAttribute<T>(string propertyName)
        {
            propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);
            MemberInfo property = typeof(T).GetProperty(propertyName);
            if (property == null)
            {
                return string.Empty;
            }

            return (property.GetCustomAttribute(typeof(ColumnAttribute)) is ColumnAttribute columnAttribute) ? columnAttribute.Name : null;
        }
    }
}
