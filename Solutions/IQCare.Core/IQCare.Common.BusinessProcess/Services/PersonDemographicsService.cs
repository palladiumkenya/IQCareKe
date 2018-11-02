using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using Serilog;
namespace IQCare.Common.BusinessProcess.Services
{
   public  class PersonDemographicsService
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private PersonEducation personeducate;
        public PersonDemographicsService(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<List<PersonEducation>> GetCurrentPersonEducation(int personId)
        {
            try
            {
                List<PersonEducation> personEducations = await _unitOfWork.Repository<PersonEducation>()
                    .Get(x => x.PersonId == personId && x.DeleteFlag == false).ToListAsync();
                return personEducations;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonEducation> AddPersonEducation(PersonEducation personEducation)
        {
            try
            {
                await _unitOfWork.Repository<PersonEducation>().AddAsync(personEducation);
                await _unitOfWork.SaveAsync();
                return personEducation;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonEducation> UpdatePersonEducation(PersonEducation pm)
        {
            try
            {
                _unitOfWork.Repository<PersonEducation>().Update(pm);
                await _unitOfWork.SaveAsync();
                return pm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonEducation> UpdatePersonEducation(int personId, string educationlevel, string educationOutcome, int userid)
        {
            try
            {


                List<PersonEducation> personEducations = await _unitOfWork.Repository<PersonEducation>()
                    .Get(x => x.PersonId == personId && x.DeleteFlag == false && x.EducationLevel == Convert.ToInt32(educationlevel)).ToListAsync();
                int educationlev = Convert.ToInt32(educationlevel);
                int educationout = Convert.ToInt32(educationOutcome);

                if (personEducations.Count > 0)
                {
                    if (!String.IsNullOrEmpty(educationlevel))
                    {
                        personEducations[0].EducationLevel = educationlev;
                    }
                    if (!string.IsNullOrEmpty(educationOutcome) || Convert.ToInt32(educationOutcome.ToString()) > 0)

                    {
                        if (!String.IsNullOrWhiteSpace(personEducations[0].EducationOutcome.ToString()))
                        {
                            if (personEducations[0].EducationOutcome != educationout)
                            {
                                personEducations[0].DeleteFlag = true;
                                _unitOfWork.Repository<PersonEducation>().Update(personEducations[0]);

                                await _unitOfWork.SaveAsync();
                                PersonEducation pe = new PersonEducation();
                                pe.PersonId = personId;
                                pe.EducationLevel = educationlev;
                                pe.EducationOutcome = educationout;
                                pe.CreatedBy = userid;
                                pe.CreateDate = DateTime.Now;
                                personeducate = await this.AddPersonEducation(pe);
                                return personeducate;
                            }
                            else
                            {
                                personEducations[0].EducationOutcome = educationout;
                                _unitOfWork.Repository<PersonEducation>().Update(personEducations[0]);

                                await _unitOfWork.SaveAsync();
                                personeducate = personEducations[0];

                            }
                        }
                        else
                        {
                            personEducations[0].EducationOutcome = educationout;
                            _unitOfWork.Repository<PersonEducation>().Update(personEducations[0]);

                            await _unitOfWork.SaveAsync();
                            personeducate = personEducations[0];
                        }

                    }
                    else
                    {
                        _unitOfWork.Repository<PersonEducation>().Update(personEducations[0]);

                        await _unitOfWork.SaveAsync();

                    }
                }
                else
                {
                    PersonEducation pe = new PersonEducation();
                    pe.PersonId = personId;
                    pe.EducationLevel = educationlev;
                    if (Convert.ToInt32(educationOutcome.ToString()) > 0)
                    {
                        pe.EducationOutcome = educationout;
                    }
                    pe.CreatedBy = userid;
                    pe.CreateDate = DateTime.Now;
                    personeducate = await this.AddPersonEducation(pe);

                }


                return personeducate;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<List<PersonOccupation>> GetCurrentOccupation(int personId)
        {
            try
            {
                var occupation = await _unitOfWork.Repository<PersonOccupation>()
                    .Get(x => x.PersonId == personId && x.DeleteFlag == false).ToListAsync();
                return occupation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonOccupation> Add(PersonOccupation personOccupation)
        {
            try
            {
                await _unitOfWork.Repository<PersonOccupation>().AddAsync(personOccupation);
                await _unitOfWork.SaveAsync();
                return personOccupation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonOccupation> Update(PersonOccupation personOccupation)
        {
            try
            {
                _unitOfWork.Repository<PersonOccupation>().Update(personOccupation);
                await _unitOfWork.SaveAsync();
                return personOccupation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonOccupation> Update(int personId, string occupation, int userid)
        {
            try
            {
                List<PersonOccupation> personOccupations = await this.GetCurrentOccupation(personId);
                PersonOccupation personOccupation = new PersonOccupation();
                if (personOccupations.Count > 0)
                {

                    personOccupations[0].Occupation = Convert.ToInt32(occupation);
                    personOccupation = await this.Update(personOccupations[0]);
                }

                else
                {
                    PersonOccupation pc = new PersonOccupation()
                    {
                        PersonId = personId,
                        Occupation = Convert.ToInt32(occupation),
                        CreateDate = DateTime.Now,
                        CreatedBy = userid,
                        Active = false,
                        DeleteFlag = false
                    };
                    personOccupation = await this.Add(pc);
                }




                return personOccupation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }



        }

    }
}

