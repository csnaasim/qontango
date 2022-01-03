using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace System.Data
{
    public static class DataExtension
    {
        public static bool HasColumn(this DataRow row, string ColumnName)
        {
            return row.Table.Columns.Contains(ColumnName);
        }

        public static bool HasColumns(this DataRow row, string [] ColumnNames) 
        {
            bool HasAColumn = false;
            foreach(var ColumnName in ColumnNames)
            {
                if(row.HasColumn(ColumnName))
                {
                    HasAColumn = true;
                    break;
                }
            }
            return HasAColumn;
        }
        
        public static T GetValue<T>(this DataRow row, string ColumnName)
        {
            try
            {
                if (row.Table.Columns.Contains(ColumnName) && row[ColumnName] != DBNull.Value)
                    return (T)Convert.ChangeType(row[ColumnName], typeof(T));
                else
                    return default(T);//any default value for the specified type will be returned
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static T? GetValueNullable<T>(this DataRow row, string ColumnName) where T : struct 
        {
            try
            {
                if (!row.Table.Columns.Contains(ColumnName) || row[ColumnName]  is DBNull) return null;
                if (row[ColumnName] is T) return (T)row[ColumnName];
                return (T)Convert.ChangeType(row[ColumnName], typeof(T));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public static T GetDataRowValue<T>(this DataRow row, string FieldName) where T : unmanaged
        //{
        //    try
        //    {
        //        if (row[FieldName] != DBNull.Value)
        //            return (T)Convert.ChangeType(row[FieldName].ToString(), typeof(T));
        //        else
        //        {
        //            return default(T);
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}



    }
}
