using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Http;
using IQCare.Queue.BusinessProcess.Command;
namespace IQCare.Queue.Web
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class QueueController : Controller
    {
        private readonly IMediator _mediator;

        public QueueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddRooms([FromBody] AddRoomCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpPost]

        public async Task<IActionResult> CheckRoomExists([FromBody] CheckRoomExistCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);

        }
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var response = await _mediator.Send(new GetRoomsCommand(), HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);
            return Ok(response.Value);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var  response =await _mediator.Send(new GetRoomByIdCommand { Id=id}, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);

        }
        [HttpGet]
        public async Task<IActionResult> GetLookupItems()
        {
            var response = await _mediator.Send(new GetQueueLookupItemsCommand(), HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);
            return Ok(response.Value);
        }
        [HttpPost]
        public async Task<IActionResult> EditRoom([FromBody] EditRoomCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }
        

        [HttpPost]
        public async Task<IActionResult> AddLinkageServiceRoom([FromBody] LinkageServiceRoomCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomsServicePointList(int id)
        {
            var results = await _mediator.Send(new GetRoomsAndServicePointCommand { ServiceArea = id }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}