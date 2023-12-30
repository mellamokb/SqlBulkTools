using System.Data;

namespace SqlBulkTools
{

    /// <summary>
    /// 
    /// </summary>
    public interface IDataTableTransaction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataTable BuildDataTable();
    }

}