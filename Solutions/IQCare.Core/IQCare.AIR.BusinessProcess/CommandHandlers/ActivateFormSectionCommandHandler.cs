using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using IQCare.AIR.BusinessProcess.Command;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Serilog;
using System.Threading.Tasks;
using System.Linq;
namespace IQCare.AIR.BusinessProcess.CommandHandlers
{
    public class ActivateFormSectionCommandHandler:IRequestHandler<ActivateFormSectionCommand , Result<ActivateFormSectionResponse>>
    {

        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<ActivateFormSectionCommandHandler>();

    

        public ActivateFormSectionCommandHandler(IAirUnitOfWork airUnitOfWork)
        {
            _airUnitOfWork = airUnitOfWork ?? throw new ArgumentNullException(nameof(airUnitOfWork));
        }


        public Task<Result<ActivateFormSectionResponse>> Handle(ActivateFormSectionCommand request, CancellationToken cancellationToken)
        {
            try
            {  
                if (request.SectionList != null)
                {
                    foreach (var section in request.SectionList)
                    {
                        var formsection = _airUnitOfWork.Repository<ReportSection>().Get(x => x.Id == section.Id).FirstOrDefault();
                        if (formsection != null)
                        {

                            if (section.Active == true)
                            {
                                formsection.UpdateSection(true);

                            }
                            else if (section.Active == false)
                            {
                                formsection.UpdateSection(false);
                            }

                            _airUnitOfWork.Repository<ReportSection>().Update(formsection);
                           

                        }

                        _airUnitOfWork.Save();


                    }


                }

                return Task.FromResult(Result<ActivateFormSectionResponse>.Valid(new ActivateFormSectionResponse
                {
                    Message = $"The settings have been updated"
                }));
            }
            catch (Exception ex)
            {
                string message = $"An error has occurred: " + ex.Message.ToString();
                _logger.Error(ex, message);
                return Task.FromResult(Result<ActivateFormSectionResponse>.Invalid(message));
            }
          
        }
    }
}
