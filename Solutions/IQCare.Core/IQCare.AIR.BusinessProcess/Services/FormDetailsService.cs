using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading.Tasks;

namespace IQCare.AIR.BusinessProcess.Services
{
    public class FormDetailsService
    {
        private readonly IAirUnitOfWork _airUnitWork;

        public FormDetailsService(IAirUnitOfWork airUnitOfWork)
        {
            _airUnitWork = airUnitOfWork ?? throw new ArgumentNullException(nameof(airUnitOfWork));
        }

        public async Task<ReportingForm> GetSpecificForm(int formId)
        {
            try
            {
                var reportingforms = await _airUnitWork.Repository<ReportingForm>().Get(x => x.Id == formId && x.DeleteFlag != true).FirstOrDefaultAsync();
                return reportingforms;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching the forms");
                throw ex;

            }
        }
        public async Task<IEnumerable<ReportingForm>> GetForms()
        {
            try
            {
                var reportingforms = await _airUnitWork.Repository<ReportingForm>().GetAllAsync();
                return reportingforms;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching the forms");
                throw ex;

            }

        }

        public async Task<List<ReportSection>> GetSections(int FormId)
        {
            try
            {
                var Sections = await _airUnitWork.Repository<ReportSection>().Get(x => x.ReportigFormId == FormId).ToListAsync();
                return Sections;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching Sections");
                throw ex;
            }

        }

        public async Task<List<ReportSubSection>> GetSubSections(int SectionId)
        {
            try
            {
                var ReportSubSection = await _airUnitWork.Repository<ReportSubSection>().Get(x => x.ReportSectionId == SectionId).ToListAsync();
                return ReportSubSection;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching subsections");
                throw ex;
            }
        }



        public async Task<List<Indicator>> GetIndicators(int SubSectionId)
        {
            try
            {
                var Indicators = await _airUnitWork.Repository<Indicator>().Get(x => x.ReportSubSectionId == SubSectionId).ToListAsync();
                return Indicators;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching Indicators");
                throw ex;
            }
        }



        public async Task<List<IndicatorResult>> GetIndicatorResults(int Id)
        {
            try
            {
                var IndicatorResults = await _airUnitWork.Repository<IndicatorResult>().Get(x => x.ReportingPeriodId == Id).ToListAsync();
                return IndicatorResults;
            }

            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching indicatorresults");
                throw ex;

            }
        }



        public async Task<ReportingPeriod> GetReportingPeriods(int Id)
        {
            try
            {
                var ReportingPeriod = await _airUnitWork.Repository<ReportingPeriod>().Get(x => x.Id == Id).FirstOrDefaultAsync();
                return ReportingPeriod;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching ReportingPeriod");
                throw ex;

            }
        }
    }
}
