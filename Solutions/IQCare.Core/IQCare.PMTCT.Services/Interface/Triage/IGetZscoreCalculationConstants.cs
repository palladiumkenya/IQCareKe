using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IQCare.PMTCT.Services.Interface.Triage
{
    public enum Sex
    {
        Male = 1,
        Female
    }

    public class LmsParameter
    {

        public object Lambda { get; set; }
        public object Median { get; set; }
        public object Sigma { get; set; }

    }

    public class SprocSearchParams
    {
        public DateTime DateOfBirth { get; set; }
        public int Height { get; set; }
        public Sex Sex { get; set; }
    }

    public interface IGetZscoreLmsParameters
    {
        LmsParameter GetLmsParameter(ZscoreType zscoreType, SprocSearchParams searchParams);

    }

    public class  GetZscoreLmsParameters : IGetZscoreLmsParameters
    {
        private readonly Func<SqlConnection> _sqlConnectionFunc;
        private readonly ConcurrentDictionary<ZscoreType, string> _zscoreSprocConcurrentDict;
        public GetZscoreLmsParameters(Func<SqlConnection> sqlConnectionFunc, ConcurrentDictionary<ZscoreType, string> zscoreSprocConcurrentDict)
        {
            _sqlConnectionFunc = sqlConnectionFunc;
            _zscoreSprocConcurrentDict = zscoreSprocConcurrentDict;
        }


        public LmsParameter GetLmsParameter(ZscoreType zscoreType, SprocSearchParams searchParams)
        {
            var sprocExists = _zscoreSprocConcurrentDict.TryGetValue(zscoreType, out string sprocName);

            return sprocExists? GetLmsParameterFromSproc(sprocName, BuildSprocSearchDict(searchParams, zscoreType))
                : null;
        }

        private Dictionary<string, object> BuildSprocSearchDict(SprocSearchParams searchParams, ZscoreType zscoreType)
        {
            switch (zscoreType)
            {
                case ZscoreType.WeightForAge:
                case ZscoreType.Bmiz:
                case ZscoreType.HeightForAge:
                    return new Dictionary<string, object>
                    {
                        {"@DateOfBirth", searchParams.DateOfBirth},
                        {"@Sex", searchParams.Sex}
                    };
                case ZscoreType.WeightForHeight:
                    return new Dictionary<string, object>
                    {
                        {"@Height", searchParams.Height},
                        {"@Sex", searchParams.Sex}
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(zscoreType), zscoreType, null);
            }
        }

        private LmsParameter GetLmsParameterFromSproc(string sprocName, Dictionary<string, object> parameters)
        {
            using (var dbConnection = _sqlConnectionFunc())
            using (var command = new SqlCommand(sprocName, dbConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                        command.Parameters[param.Key].Direction = ParameterDirection.Input;
                    }
                    command.Connection.Open();
                    LmsParameter lmsParameter = null;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows && reader.FieldCount > 1)
                        {
                            while (reader.Read())
                            {
                                var name = reader.GetDataTypeName(2);
                                lmsParameter = new LmsParameter()
                                {
                                   
                                    Lambda = reader.GetValue(2),
                                    Median = reader.GetValue(3),
                                    Sigma =  reader.GetValue(4)
                                };
                            }
                            break;
                        }
                    }

                    return lmsParameter;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
