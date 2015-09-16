// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.SqlServer.FunctionalTests.TestModels;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.SqlServer.FunctionalTests
{
    public class NorthwindQuerySqlServerFixture : NorthwindQueryRelationalFixture, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextOptions _options;
        private readonly SqlServerTestStore _testStore;

        public NorthwindQuerySqlServerFixture()
        {
            _testStore = SqlServerNorthwindContext.GetSharedStore();

            _serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddSqlServer()
                .ServiceCollection()
                .AddSingleton(TestSqlServerModelSource.GetFactory(OnModelCreating))
                .AddInstance<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();

            _options = ConfigureOptions();

            _serviceProvider.GetRequiredService<ILoggerFactory>().MinimumLevel = LogLevel.Debug;
        }

        protected virtual DbContextOptions ConfigureOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            var sqlOptions = optionsBuilder.UseSqlServer(_testStore.Connection.ConnectionString).ApplyConfiguration();

            return optionsBuilder.Options;
        }

        public override NorthwindContext CreateContext() => CreateContext(useRelationalNulls: false);

        public override NorthwindContext CreateContext(bool useRelationalNulls)
        {
            RelationalOptionsExtension.Extract(_options).UseRelationalNulls = useRelationalNulls;

            var context = new SqlServerNorthwindContext(_serviceProvider, _options);

            context.ChangeTracker.AutoDetectChangesEnabled = false;

            return context;
        }

        public void Dispose() => _testStore.Dispose();
    }
}
