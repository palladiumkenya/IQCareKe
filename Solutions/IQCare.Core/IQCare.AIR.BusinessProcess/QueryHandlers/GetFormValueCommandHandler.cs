using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Queries;
using IQCare.AIR.BusinessProcess.Services;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.AIR.BusinessProcess.QueryHandlers
{
    public class GetFormValueCommandHandler:IRequestHandler<GetFormValueQuery,Result<GetFormValueResponse>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;

        public GetFormValueCommandHandler(IAirUnitOfWork airUnitOfWork)
        {
            _airUnitOfWork = airUnitOfWork ?? throw new ArgumentNullException(nameof(airUnitOfWork));
        }

        public async Task<Result<GetFormValueResponse>> Handle(GetFormValueQuery request,CancellationToken cancellationToken )
        {
            try
            {
                FormDetailsService fds = new FormDetailsService(_airUnitOfWork);
               // ReportingValues rs = new ReportingValues();
                List<ReportingValues> rlist = new List<ReportingValues>();

                if (request.Id > 0)
                {
                    var ReportingForm = await Task.Run(() => fds.GetReportingPeriods(request.Id));
                    if (ReportingForm != null)
                    {
                        var Indicators = await Task.Run(() => fds.GetIndicatorResults(ReportingForm.Id));
                        if (Indicators != null)
                        {

                           
                            Indicators.ForEach(x =>
                            {
                                ReportingValues rs = new ReportingValues();
                                rs.ReportingFormId = x.ReportingPeriod.ReportingFormId;
                                rs.ReportingId = x.Id;
                                rs.ReportDate = x.ReportingPeriod.ReportDate;
                                rs.IndicatorId = x.IndicatorId;
                                rs.ReportingPeriodId = x.ReportingPeriodId;
                                rs.ResultNumeric = x.ResultNumeric;
                                rs.ResultText = x.ResultText;

                                rlist.Add(rs);


                            }

                            );



                        }

                    }
                }


                return Result<GetFormValueResponse>.Valid(new GetFormValueResponse
                {
                    reportingValues = rlist

                });
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return Result<GetFormValueResponse>.Invalid(ex.Message);
            }
        }
    }


}
