using System.Data;

namespace SqlBulkTools
{

    /// <summary>
    /// 
    /// </summary>
    public interface IDataTableOperations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        SetupDataTable<T> SetupDataTable<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataTable BuildPreparedDataDable();
    }

}