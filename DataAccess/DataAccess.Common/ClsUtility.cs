using System;
using System.Collections;
using System.Data;

namespace DataAccess.Common
{
    public class ClsUtility
    {
        private static int Pkey;
        public static Hashtable theParams = new Hashtable();

        public enum ObjectEnum
        {
            DataSet,
            DataTable,
            DataRow,
            ExecuteNonQuery
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
        public static void AddParameters(string parameterName, SqlDbType sqlDbType, string fieldValue, ParameterDirection direction = ParameterDirection.Input)
        {
            if (sqlDbType == SqlDbType.DateTime)//conversion of string to date time...using ISO standard for datetime defination always
            {
                DateTime dateValue;
                if (DateTime.TryParse(fieldValue, out dateValue))
                {
                    fieldValue = dateValue.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");

                }

            }
            System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter(parameterName,fieldValue);
            Pkey = Pkey + 1;
            theParams.Add(Pkey, p);
          
            
            //theParams.Add(Pkey, FieldName);
            //Pkey = Pkey + 1;
            //theParams.Add(Pkey, FieldType);
            //Pkey = Pkey + 1;

            //if (FieldType == SqlDbType.DateTime)//conversion of string to date time...using ISO standard for datetime defination always
            //{
            //    DateTime dateValue;
            //    if (DateTime.TryParse(FieldValue, out dateValue))
            //        FieldValue = dateValue.ToString("yyyyMMdd hh:mm:ss tt");

            //}
            // theParams.Add(Pkey, FieldValue);
        }
        /// <summary>
        /// Adds the parameters.
        /// </summary>
        /// <param name="FieldName">Name of the field.</param>
        /// <param name="FieldType">Type of the field.</param>
        /// <param name="FieldValue">The field value.</param>
        public static void AddExtendedParameters(string parameterName, SqlDbType sqlDbType, object fieldValue, ParameterDirection direction = ParameterDirection.Input)
        {
            /* Pkey = Pkey + 1;
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
             theParams.Add(Pkey, FieldValue);*/

            System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter(parameterName, sqlDbType);

            if (sqlDbType == SqlDbType.DateTime)//conversion of string to date time...using ISO standard for datetime defination always
            {
                DateTime dateValue;
                if (DateTime.TryParse(fieldValue.ToString(), out dateValue))
                {
                    fieldValue = (dateValue.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                  
                }

                }
            p.Value = fieldValue;
            p.Direction = direction;
            Pkey = Pkey + 1;
            theParams.Add(Pkey, p);
        }
    }
}
