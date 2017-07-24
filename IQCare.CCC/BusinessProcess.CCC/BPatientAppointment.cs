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
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientAppointmentRepository.Add(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
   
        }

        public PatientAppointment GetPatientAppointments(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientAppointment appointment = _unitOfWork.PatientAppointmentRepository.GetById(id);
                _unitOfWork.Dispose();
                return appointment;
            }
          
        }

        public List<BlueCardAppointment> GetBluecardPatientAppointmentsBypatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<BlueCardAppointment> appointments = _unitOfWork.BluecardAppointmentRepository.GetBluecardPatientAppointmentsBypatientId(patientId);
                _unitOfWork.Dispose();
                return appointments;
            }

        }

        public void DeletePatientAppointments(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientAppointment appointment = _unitOfWork.PatientAppointmentRepository.GetById(id);
                _unitOfWork.PatientAppointmentRepository.Remove(appointment);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }
        
        }

        public int UpdatePatientAppointments(PatientAppointment p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientAppointmentRepository.Update(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        
        }

        public List<PatientAppointment> GetByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientAppointment> appointments =
                       _unitOfWork.PatientAppointmentRepository.GetByPatientId(patientId);
                _unitOfWork.Dispose();
                return appointments;
            }

        }

        public List<PatientAppointment> GetByDate(DateTime date)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientAppointment> appointments = _unitOfWork.PatientAppointmentRepository.GetByDate(date);
                _unitOfWork.Dispose();
                return appointments;
            }
          
        }

        public List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientAppointment> appointments =
                        _unitOfWork.PatientAppointmentRepository.GetByDateRange(startDate, endDate);
                _unitOfWork.Dispose();
                return appointments;
            }
        }
    }
}