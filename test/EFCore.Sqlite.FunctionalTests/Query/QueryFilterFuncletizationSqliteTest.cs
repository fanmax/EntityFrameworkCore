﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class QueryFilterFuncletizationSqliteTest
        : QueryFilterFuncletizationTestBase<QueryFilterFuncletizationSqliteTest.QueryFilterFuncletizationSqliteFixture>
    {
        public QueryFilterFuncletizationSqliteTest(
            QueryFilterFuncletizationSqliteFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        [Fact(Skip = "#11879")]
        public override void Using_DbSet_in_filter_works()
        {
            base.Using_DbSet_in_filter_works();
        }

        [Fact(Skip = "#11879")]
        public override void Using_Context_set_method_in_filter_works()
        {
            base.Using_Context_set_method_in_filter_works();
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        public class QueryFilterFuncletizationSqliteFixture : QueryFilterFuncletizationRelationalFixture
        {
            protected override ITestStoreFactory TestStoreFactory => SqliteTestStoreFactory.Instance;
        }
    }
}
