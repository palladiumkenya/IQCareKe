using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class ClsUtility
    {
        private static int Pkey;
        public static Hashtable theParams = new Hashtable();

        public enum ObjectEnum
        {
            DataSet,DataTable,DataRow,ExecuteNonQuery
        }

        public static void Init_Hashtable()
        {
            //theParams.Clear();
            theParams = new Hashtable();
            Pkey = 0;
        }
        /// <summary>
        /// Adds the parameters.
        /// </summary>
        /// <param name="FieldName">Name of the field.</param>
        /// <param name="FieldType">Type of the field.</param>
        /// <param name="FieldValue">The field value.</param>
        public static void AddParameters(string FieldName,SqlDbType FieldType,string FieldValue, ParameterDirection Direction = ParameterDirection.Input)
        {
            Pkey = Pkey + 1;
            theParams.Add(Pkey, FieldName);
            Pkey = Pkey + 1;
            theParams.Add(Pkey, FieldType);
            Pkey = Pkey + 1;
            
            if (FieldType == SqlDbType.DateTime)//conversion of string to date time...using ISO standard for datetime defination always
            {
                DateTime dateValue;
                if (DateTime.TryParse(FieldValue,out dateValue))
                    FieldValue=dateValue.ToString("yyyyMMdd hh:mm:ss tt");
             
            }
            theParams.Add(Pkey, FieldValue);
        }
        /// <summary>
        /// Adds the parameters.
        /// </summary>
        /// <param name="FieldName">Name of the field.</param>
        /// <param name="FieldType">Type of the field.</param>
        /// <param name="FieldValue">The field value.</param>
        public static void AddExtendedParameters(string FieldName, SqlDbType FieldType, object FieldValue, ParameterDirection Direction = ParameterDirection.Input)
        {
            Pkey = Pkey + 1;
            theParams.Add(Pkey, FieldName);
            Pkey = Pkey + 1;
            theParams.Add(Pkey, FieldType);

            Pkey = Pkey + 1;
            if (FieldType == SqlDbType.DateTime)//conversion of string to date time...using ISO standard for datetime defination always
            {
                DateTime dateValue;
                if (DateTime.TryParse(FieldValue.ToString(), out dateValue))
                    FieldValue = dateValue.ToString("yyyyMMdd hh:mm:ss tt");
            }
            theParams.Add(Pkey, FieldValue);
        }
    }
}
