using System.Data;
namespace IQCare.Web
{

    /// <summary>
    /// Summary description for AuthenticationManager
    /// </summary>
    public class AuthenticationManager
    {
        #region "Constructor"
        public AuthenticationManager()
        {
        }
        #endregion

        #region "Application Parameters"
        //  public static string AppVersion = "Ver 3.5.3.1 Kenya HMIS";
        // public static string ReleaseDate = "01-Jun-2016";
        #endregion

        /* public Boolean HasFeatureRightByReferenceID(string ReferenceID, DataTable theDT)
    {
        DataView theDV = new DataView(theDT);
        theDV.RowFilter = "ReferenceID = '" + ReferenceID + "'";
        if (theDV.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }*/
        public bool HasFunctionRight(string referenceId, int functionId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "ReferenceID = '" + referenceId + "' and FunctionId = " + functionId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasFeatureRight(int featureId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "FeatureId = " + featureId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasFeatureRight(string referenceId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "ReferenceID = '" + referenceId + "'";
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasFunctionRight(int featureId, int functionId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "FeatureId = " + featureId.ToString() + " and FunctionId = " + functionId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasModuleRight(int moduleId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "ModuleId = " + moduleId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}