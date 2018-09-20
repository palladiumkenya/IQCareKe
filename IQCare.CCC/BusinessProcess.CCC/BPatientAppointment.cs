using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Appointment;
using Interface.CCC;
using System;
using System.Collections.Generic;

namespace BusinessProcess.CCC
{
    public class BPatientAppointment : ProcessBase, IPatientAppointment
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientAppointments(PatientAppointment p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientAppointmentRepository.Add(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return p.Id;
            }
   
        }

        public PatientAppointment GetPatientAppointments(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientAppointment appointment = unitOfWork.PatientAppointmentRepository.GetById(id);
                unitOfWork.Dispose();
                return appointment;
            }
          
        }

        public List<BlueCardAppointment> GetBluecardPatientAppointmentsBypatientId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<BlueCardAppointment> appointments = unitOfWork.BluecardAppointmentRepository.GetBluecardPatientAppointmentsBypatientId(patientId);
                unitOfWork.Dispose();
                return appointments;
            }

        }

        public void DeletePatientAppointments(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientAppointment appointment = unitOfWork.PatientAppointmentRepository.GetById(id);
                unitOfWork.PatientAppointmentRepository.Remove(appointment);
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        
        }

        public int UpdatePatientAppointments(PatientAppointment p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientAppointmentRepository.Update(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        
        }

        public List<PatientAppointment> GetByPatientId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientAppointment> appointments =
                       unitOfWork.PatientAppointmentRepository.GetByPatientId(patientId);
                unitOfWork.Dispose();
                return appointments;
            }

        }

        public List<PatientAppointment> GetByDate(DateTime date)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientAppointment> appointments = unitOfWork.PatientAppointmentRepository.GetByDate(date);
                unitOfWork.Dispose();
                return appointments;
            }
          
        }

        public List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientAppointment> appointments =
                        unitOfWork.PatientAppointmentRepository.GetByDateRange(startDate, endDate);
                unitOfWork.Dispose();
                return appointments;
            }
        }

        public List<AppointmentSummary> GetAppointmentSummaryByDate(DateTime date)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<AppointmentSummary> summary = unitOfWork.PatientAppointmentRepository.GetAppointmentSummaryByDate(date);
                unitOfWork.Dispose();
                return summary;
            }
        }
    }
}