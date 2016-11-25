using System.Collections.Generic;
using Entities.Lab;

namespace Interface.Laboratory
{
    /// <summary>
    /// Lab Test management interface
    /// </summary>
    public interface ILabManager
    {

        int DeleteLabTest(int labTestId, int userId);
        /// <summary>
        /// Inactivate/Activate the lab test.
        /// </summary>
        /// <param name="labTestId">The lab test identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>        
        int ActivateLabTest(int labTestId, bool active, int userId);
        /// <summary>
        /// Deletes the test parameter.
        /// </summary>
        /// <param name="parameterId">The parameter identifier.</param>
        /// <param name="labTestid">The lab testid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int DeleteTestParameter(int parameterId, int labTestId, int userId);

        /// <summary>
        /// Gets the lab test by identifier.
        /// </summary>
        /// <param name="labTestId">The lab test identifier.</param>
        /// <returns></returns>
        LabTest GetLabTestById(int labTestId);

        /// <summary>
        /// Gets the lab test parameter by identifier.
        /// </summary>
        /// <param name="LabTestId">The lab test identifier.</param>
        /// <param name="ParameterId">The parameter identifier.</param>
        /// <returns></returns>
        TestParameter GetLabTestParameterById(int LabTestId, int ParameterId);

        /// <summary>
        /// Gets the lab test parameters.
        /// </summary>
        /// <param name="LabTestId">The lab test identifier.</param>
        /// <returns></returns>
        List<TestParameter> GetLabTestParameters(int LabTestId);

        List<TestDepartment> GetTestDepartments();
        /// <summary>
        /// Gets the lab tests.
        /// </summary>
        /// <returns></returns>
        List<LabTest> GetLabTests();

       LabTestGroup GetGroupLabTest(int mainTestId);
       void SaveGroupLabTest(int labTestId, int mainTestId);
        void RemoveTestFromGroup(int labTestId, int mainTestId);
        /// <summary>
        /// Gets the parameter configuration.
        /// </summary>
        /// <param name="ParameterId">The parameter identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        List<ParameterResultConfig> GetParameterConfig(int ParameterId);

        /// <summary>
        /// Gets the parameter result option.
        /// </summary>
        /// <param name="ParameterId">The parameter identifier.</param>
        /// <returns></returns>
        List<ParameterResultOption> GetParameterResultOption(int ParameterId);

        /// <summary>
        /// Gets the result units.
        /// </summary>
        /// <returns></returns>
        List<ResultUnit> GetResultUnits();

        /// <summary>
        /// Saves the lab test.
        /// </summary>
        /// <param name="labTest">The lab test.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        LabTest SaveLabTest(LabTest labTest, int userId);

        /// <summary>
        /// Saves the lab test parameter.
        /// </summary>
        /// <param name="testParameters">The test parameters.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        TestParameter SaveLabTestParameter(TestParameter testParameters, int UserId);
    }
}