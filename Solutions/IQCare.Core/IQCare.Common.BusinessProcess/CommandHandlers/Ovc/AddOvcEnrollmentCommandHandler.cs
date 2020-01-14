using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Ovc;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Ovc
{
    public class AddOvcEnrollmentCommandHandler : IRequestHandler<AddOvcEnrollmentCommand, Result<Response>>
    {
        public string Message { get; set; }
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddOvcEnrollmentCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<Response>> Handle(AddOvcEnrollmentCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _unitOfWork.Repository<OvcEnrollmentForm>().Get(x => x.PersonId == request.PersonId &&
                x.EnrollmentDate.Value.Day == request.EnrollmentDate.Value.Day && x.EnrollmentDate.Value.Year == request.EnrollmentDate.Value.Year
                && x.EnrollmentDate.Value.Month == request.EnrollmentDate.Value.Month && x.DeleteFlag != true).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if(result != null)
                {
                    result.EnrollmentDate = request.EnrollmentDate;
                    result.CPMISEnrolled = request.CPMISEnrolled;
                    result.PartnerOVCServices = request.PartnerOVCServices;
                    result.CreatedBy = request.CreatedBy;
                    result.DeleteFlag = false;
                    result.CreateDate = DateTime.Now;
                    result.PersonId = request.PersonId;

                     _unitOfWork.Repository<OvcEnrollmentForm>().Update(result);
                    await _unitOfWork.SaveAsync();
                    Message += "The information has been updated successfully";

                }
                else
                {
                    OvcEnrollmentForm enroll = new OvcEnrollmentForm();
                    enroll.EnrollmentDate = request.EnrollmentDate;
                    enroll.CPMISEnrolled = request.CPMISEnrolled;
                    enroll.PartnerOVCServices = request.PartnerOVCServices;
                    enroll.CreatedBy = request.CreatedBy;
                    enroll.DeleteFlag = false;
                    enroll.CreateDate = DateTime.Now;
                    enroll.PersonId = request.PersonId;

                     await  _unitOfWork.Repository<OvcEnrollmentForm>().AddAsync(enroll);
                    await _unitOfWork.SaveAsync();
                    Message += "The information has been successfully added";

                }

                return Result<Response>.Valid(new Response { Message=Message });
            }
            catch (Exception ex)
            {
                return Result<Response>.Invalid(ex.Message);

            }

        }

    }
}