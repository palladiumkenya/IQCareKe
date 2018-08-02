using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.PMTCT.ANC
{
    [Produces("application/json")]
    [Route("api/VisitDetails")]
    public class VisitDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public VisitDetailsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody]VisitDetailsCommand visitDetailsCommand)
        //{

        //}
    }
}