using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.Services
{
    public class EducationLevelService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;

        private PersonEducation personeducate;
        public EducationLevelService(ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<List<PersonEducation>> GetCurrentPersonEducation(int personId)
        {
            try
            {
                List<PersonEducation> personEducations = await _commonUnitOfWork.Repository<PersonEducation>()
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
                await _commonUnitOfWork.Repository<PersonEducation>().AddAsync(personEducation);
                await _commonUnitOfWork.SaveAsync();
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
                _commonUnitOfWork.Repository<PersonEducation>().Update(pm);
                await _commonUnitOfWork.SaveAsync();
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


                List<PersonEducation> personEducations = await _commonUnitOfWork.Repository<PersonEducation>()
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
                                _commonUnitOfWork.Repository<PersonEducation>().Update(personEducations[0]);

                                await _commonUnitOfWork.SaveAsync();
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
                                _commonUnitOfWork.Repository<PersonEducation>().Update(personEducations[0]);

                                await _commonUnitOfWork.SaveAsync();
                                personeducate = personEducations[0];

                            }
                        }
                        else
                        {
                            personEducations[0].EducationOutcome = educationout;
                            _commonUnitOfWork.Repository<PersonEducation>().Update(personEducations[0]);

                            await _commonUnitOfWork.SaveAsync();
                            personeducate = personEducations[0];
                        }

                    }
                    else
                    {
                        _commonUnitOfWork.Repository<PersonEducation>().Update(personEducations[0]);

                        await _commonUnitOfWork.SaveAsync();

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
    }
}