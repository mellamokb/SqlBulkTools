global using System.Data;
#if RELEASESYSTEMDATA
global using System.Data.SqlClient;
#else
global using Microsoft.Data.SqlClient;
#endif
global using System.Transactions;
global using AutoFixture;
global using SqlBulkTools.Enumeration;
global using SqlBulkTools.IntegrationTests.Data;
global using SqlBulkTools.TestCommon;
global using SqlBulkTools.TestCommon.Model;
global using Xunit;
