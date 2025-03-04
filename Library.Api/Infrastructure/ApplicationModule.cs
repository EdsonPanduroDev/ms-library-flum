using Autofac;
using Library.Application.Generic;
using Library.Application.Queries.Implementations;
using Library.Application.Queries.Interface;
using Library.Domain.Aggregates.Rental;
using Library.Repository.Repositories;

namespace Library.Api.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        public string _connectionString { get; }
        public string _timeZone { get; set; }
        public ApplicationModule(string connectionString, string timeZone)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _timeZone = timeZone ?? throw new ArgumentNullException(nameof(_timeZone));
        }
        protected override void Load(ContainerBuilder builder)
        {
            #region Generic
            builder.Register(c => new ValuesSettings(_timeZone)).As<IValuesSettings>().InstancePerLifetimeScope();
            builder.Register(c => new GenericQuery(_connectionString)).As<IGenericQuery>().InstancePerLifetimeScope();
            #endregion

            #region Repositories
            builder.Register(c => new RentalRepository(_connectionString)).As<IRentalRepository>().InstancePerLifetimeScope();
            #endregion

            #region Queries
            builder.RegisterType<BookCopyQuery>().As<IBookCopyQuery>();
            #endregion
        }
    }
}
