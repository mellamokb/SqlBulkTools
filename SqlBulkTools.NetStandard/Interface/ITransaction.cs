﻿using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace SqlBulkTools
{

    internal interface ITransaction
    {
        int Commit(IDbConnection connection, IDbTransaction transaction = null);

        int Commit(SqlConnection connection, SqlTransaction transaction);

        Task<int> CommitAsync(SqlConnection connection, SqlTransaction transaction, CancellationToken cancellationToken);
    }

}