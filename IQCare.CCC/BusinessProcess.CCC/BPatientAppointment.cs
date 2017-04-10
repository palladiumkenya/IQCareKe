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
            try
            {
                _unitOfWork.PatientAppointmentRepository.Add(p);
                _result = _unitOfWork.Complete();
                return _result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
   
        }

        public PatientAppointment GetPatientAppointments(int id)
        {
            try
            {
                PatientAppointment appointment = _unitOfWork.PatientAppointmentRepository.GetById(id);
                return appointment;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
          
        }

        public void DeletePatientAppointments(int id)
        {
            try
            {
                PatientAppointment appointment = _unitOfWork.PatientAppointmentRepository.GetById(id);
                _unitOfWork.PatientAppointmentRepository.Remove(appointment);
                _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        
        }

        public int UpdatePatientAppointments(PatientAppointment p)
        {
            try
            {
                _unitOfWork.PatientAppointmentRepository.Update(p);
                _result = _unitOfWork.Complete();
                return _result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        
        }

        public List<PatientAppointment> GetByPatientId(int patientId)
        {
            try
            {
                List<PatientAppointment> appointments =
                    _unitOfWork.PatientAppointmentRepository.GetByPatientId(patientId);
                return appointments;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

        public List<PatientAppointment> GetByDate(DateTime date)
        {
            try
            {
                List<PatientAppointment> appointments = _unitOfWork.PatientAppointmentRepository.GetByDate(date);
                return appointments;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
          
        }

        public List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<PatientAppointment> appointments =
                    _unitOfWork.PatientAppointmentRepository.GetByDateRange(startDate, endDate);
                return appointments;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}