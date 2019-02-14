using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Command;
using IQCare.AIR.BusinessProcess.Services;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.AIR.BusinessProcess.CommandHandlers
{
    public class GetFormDetailsCommandHandler : IRequestHandler<GetFormDetailsCommand, Result<GetFormDetailsResultResponse>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;


        public GetFormDetailsCommandHandler(IAirUnitOfWork airUnitOfWork)
        {
            _airUnitOfWork = airUnitOfWork ?? throw new ArgumentNullException(nameof(airUnitOfWork));
        }


        public async Task<Result<GetFormDetailsResultResponse>>Handle(GetFormDetailsCommand request,CancellationToken cancellationToken)
        {
            try
            {
                FormDetailsService formsdetailservice = new FormDetailsService(_airUnitOfWork);
                ReportingForm FormDetails = new ReportingForm();
                List<ReportSection> rs = new List<ReportSection>();
                List<ReportSubSection> rss = new List<ReportSubSection>();
                List<ReportSubSection> reportsubsections = new List<ReportSubSection>();
                List<Indicator> ids = new List<Indicator>();
                IndicatorDetails idl = new IndicatorDetails();
                List<IndicatorDetails> idls=new List<IndicatorDetails>()l

                
                if (request.FormId > 0)
                {
                    FormDetails = await Task.Run(() => formsdetailservice.GetSpecificForm(request.FormId));

                    if (FormDetails != null)
                    {
                        rs = await Task.Run(() => formsdetailservice.GetSections(request.FormId));

                        if (rs != null)
                        {
                            foreach (var reportsection in rs)
                            {
                                rss = await Task.Run(() => formsdetailservice.GetSubSections(reportsection.Id));
                                reportsubsections.AddRange(rss);

                            }
                        }

                        if (reportsubsections != null)
                        {
                            reportsubsections.ForEach(async (x) =>
                            {
                                var Indicator = await Task.Run(() => formsdetailservice.GetIndicators(x.Id));
                                ids.AddRange(Indicator);
                            });

                        }

                        if (ids != null)
                        {
                            ids.ForEach((x) =>
                            {
                                idl.Id = x.Id;
                                idl.IndicatorDataTypeName = x.DataType.Type;
                                idl.Name = x.Name;
                                idl.ReportSubSectionId = x.ReportSubSection.Id;
                                idl.ReportSubSectionName = x.ReportSubSection.Name;
                                idl.SectionId = x.ReportSubSection.ReportSection.Id;
                                idl.SectionName = x.ReportSubSection.ReportSection.Name;
                                idl.FormId = x.ReportSubSection.ReportSection.ReportigFormId;


                                idls.Add(idl);
                            });
                        }
                    }
                    
                    
                }
                return Result<GetFormDetailsResultResponse>.Valid(new GetFormDetailsResultResponse

                {
                    reportingForm = FormDetails,
                    ReportSections = rs,
                    ReportSubSections= reportsubsections,
                    IndicatorDetails=idls

                });
               

            }
            catch (Exception ex){

                Log.Error(ex.Message);
                return Result<GetFormDetailsResultResponse>.Invalid(ex.Message);
            }

        }
    }
}
