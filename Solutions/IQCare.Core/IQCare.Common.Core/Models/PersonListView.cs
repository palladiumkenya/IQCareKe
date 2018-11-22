using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public  class PersonListView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Sex { get; set; }
        public string Gender { get; set; }
        public bool DeleteFlag { get; set; }
        public string IdentifierValue { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string Age;
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
