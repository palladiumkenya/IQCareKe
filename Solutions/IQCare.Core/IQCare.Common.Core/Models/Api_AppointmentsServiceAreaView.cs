using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class Api_AppointmentsServiceAreaView
    {
        [Key]
        public long Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }

        public int ServiceAreaId { get; set; }
        public string AppointmentReason { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; }
        public string AppointmentType { get; set; }
        public string Description { get; set; }
        public DateTime AppDate;

        public void ConvertToDate()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.AppointmentDate))
                {
                    var convertedDate = DateTime.ParseExact(this.AppointmentDate, "yyyyMMdd", null);

                    this.AppDate = convertedDate;
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
