using System;

namespace IQCare.Common.Core.Models
{
    public class PatientsView
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ptn_pk { get; set; }
       public string EnrollmentNumber { get; set; }
       public int PatientIndex { get; set; }
       public string FirstName { get; set; }
       public string MiddleName { get; set; }
       public string LastName { get; set; }
      public int Sex { get; set; }
      public int Active { get; set; }
      public DateTime RegistrationDate { get; set; }
      public DateTime EnrollmentDate { get; set; }
      public string PatientStatus { get; set; }
      public string ExitReason { get; set; }
      public DateTime DateOfBirth { get; set; }
      public int? NationalId { get; set; }
      public int FacilityId { get; set; }
      public int PatientType { get; set; }
      public int? TransferIn { get; set; }
      public string MobileNumber { get; set; }
      public int TBStatus { get; set; }
      public int NutritionStatus { get; set; }
      public int Categorization { get; set; }
      public int DobPrecision { get; set; }
     public string Age { get; set; }
     public int AgeNumber;

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
