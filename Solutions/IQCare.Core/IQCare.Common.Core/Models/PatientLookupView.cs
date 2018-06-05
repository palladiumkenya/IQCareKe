using System;

namespace IQCare.Common.Core.Models
{
    public class PatientLookupView
    {
        public Int64 RowID { get; set; }
        public int PersonId { get; set; }

        public int? PatientId { get; set; }

        public int? ptn_pk { get; set; }

        public string FirstName { get; set; }

        public string MidName { get; set; }

        public string LastName { get; set; }

        public int Sex { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool? DobPrecision { get; set; }

        public string PatientType { get; set; }

        public string NationalId { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public string IdentifierValue { get; set; }

        public int? ServiceAreaId { get; set; }

        public string ServiceAreaName { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public int? MaritalStatusId { get; set; }
        public string MaritalStatusName { get; set; }
        public string LandMark { get; set; }

        public string Age;

        public int AgeNumber;

        public string IsHtsEnrolled;

        public void CheckIsHtsEnrolled()
        {
            this.IsHtsEnrolled = "Not Enrolled";
            if (this.PatientId != null && this.ServiceAreaId == 2)
            {
                this.IsHtsEnrolled = "Enrolled";
            }
        }

        public void CalculateYourAge()
        {
            if (this.DateOfBirth.HasValue)
            {
                DateTime DateOfBirth = this.DateOfBirth.Value;

                int result = DateTime.Compare(DateOfBirth, DateTime.Now);
                if (result > 0)
                {
                }
                else
                {
                    DateTime Now = DateTime.Now;
                    int Years = new DateTime(DateTime.Now.Subtract(DateOfBirth).Ticks).Year - 1;
                    DateTime PastYearDate = DateOfBirth.AddYears(Years);
                    int Months = 0;
                    for (int i = 1; i <= 12; i++)
                    {
                        if (PastYearDate.AddMonths(i) == Now)
                        {
                            Months = i;
                            break;
                        }
                        else if (PastYearDate.AddMonths(i) >= Now)
                        {
                            Months = i - 1;
                            break;
                        }
                    }

                    this.Age = String.Format("{0} Year(s) {1} Month(s)", Years, Months);
                    if (Months > 0)
                        this.AgeNumber = Years + 1;
                    else
                        this.AgeNumber = Years;
                }
            }
        }
    }
}