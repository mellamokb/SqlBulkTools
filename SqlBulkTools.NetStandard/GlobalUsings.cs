global using System.Data;
global using System.Linq.Expressions;
#if RELEASESYSTEMDATA
global using System.Data.SqlClient;
#else
global using Microsoft.Data.SqlClient;
#endif
global using SqlBulkTools.Enumeration;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SqlBulkTools.NetStandard.IntegrationTests")]
