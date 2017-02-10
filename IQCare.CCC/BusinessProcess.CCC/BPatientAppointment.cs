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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientAppointments(PatientAppointment p)
        {
            _unitOfWork.PatientAppointmentRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientAppointment GetPatientAppointments(int id)
        {
            PatientAppointment appointment = _unitOfWork.PatientAppointmentRepository.GetById(id);
            return appointment;
        }

        public void DeletePatientAppointments(int id)
        {
            PatientAppointment appointment = _unitOfWork.PatientAppointmentRepository.GetById(id);
            _unitOfWork.PatientAppointmentRepository.Remove(appointment);
            _unitOfWork.Complete();
        }

        public int UpdatePatientAppointments(PatientAppointment p)
        {
            _unitOfWork.PatientAppointmentRepository.Update(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public List<PatientAppointment> GetByPatientId(int patientId)
        {
            List<PatientAppointment> appointments = _unitOfWork.PatientAppointmentRepository.GetByPatientId(patientId);
            return appointments;
        }

        public List<PatientAppointment> GetByDate(DateTime date)
        {
            List<PatientAppointment> appointments = _unitOfWork.PatientAppointmentRepository.GetByDate(date);
            return appointments;
        }

        public List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            List<PatientAppointment> appointments = _unitOfWork.PatientAppointmentRepository.GetByDateRange(startDate, endDate);
            return appointments;
        }
    }
}