using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentValidation;
using IQCare.AIR.BusinessProcess.Command;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IQCare.AIR.BusinessProcess.Validators
{
    public class SubmitIndicatorResultsCommandValidator : AbstractValidator<SubmitIndicatorResultsCommand>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        public SubmitIndicatorResultsCommandValidator(IAirUnitOfWork airUnitOfWork)
        {
            _airUnitOfWork = airUnitOfWork;

            RuleFor(x => x).Custom((result, context) =>
            {
                if (result.IndicatorResults == null || !result.IndicatorResults.Any())
                {
                    context.AddFailure("Please ensure indicator result values are provided before submitting");
                     return;
                }

                var reportPeriodExists = _airUnitOfWork.Repository<ReportingPeriod>().Get(x =>
                        x.ReportDate.Month == result.ReportingDate.Month &&
                        x.ReportDate.Year == result.ReportingDate.Year).Any();

                if (reportPeriodExists)
                {
                    var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(result.ReportingDate.Month);
                    context.AddFailure($"Indicator results for the period of" +
                                       $" {month}/{result.ReportingDate.Year} already exists");
                }
                   

                var emptyIndicatorIds = result.IndicatorResults
                    .Where(x => string.IsNullOrEmpty(x.ResultText) && !x.ResultNumeric.HasValue)
                    .Select(i => i.Id)
                    .ToList();

                if(!emptyIndicatorIds.Any())
                     return;

                var indicatorDetails = _airUnitOfWork.Repository<Indicator>().Get(x => emptyIndicatorIds.Contains(x.Id))
                    .Include(x => x.ReportSubSection).Select(x => new
                    {
                        x.Name,
                        SubSection = x.ReportSubSection.Name
                    }).ToList();

                foreach (var missingIndicator in indicatorDetails)
                {
                    context.AddFailure($"Please input result value for {missingIndicator.Name} under subsection {missingIndicator.SubSection}");
                }
                   
            });
        }

        
    }
}
