using System.Data.SqlClient;
using System;

namespace ClassLibrary2.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static int GetSafeInt(this SqlDataReader reader, string name)
        {
            var idx = reader.GetOrdinal(name);
            return !reader.IsDBNull(idx) ? reader.GetInt32(idx) : 0;
        }

        public static bool GetSafeBool(this SqlDataReader reader, string name)
        {
            var idx = reader.GetOrdinal(name);
            return !reader.IsDBNull(idx) && reader.GetBoolean(idx);
        }

        public static DateTime? GetSafeDateTimeNullable(this SqlDataReader reader, string name)
        {
            var idx = reader.GetOrdinal(name);
            return !reader.IsDBNull(idx) ? reader.GetDateTime(idx) : (DateTime?)null;
        }
    }
}
