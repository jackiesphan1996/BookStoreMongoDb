using System;
using System.Threading.Tasks;
using BookStoreMongoDb.Server.Application.Features.Authors.CreateAuthor;
using BookStoreMongoDb.Server.Application.Features.Authors.DeleteAuthor;
using BookStoreMongoDb.Server.Application.Features.Authors.GetAllAuthors;
using BookStoreMongoDb.Server.Application.Features.Authors.UpdateAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMongoDb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors([FromQuery] GetAllAuthorsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> CreateAuthor([FromBody] UpdateAuthorCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] string id)
        {
            return Ok(await _mediator.Send(new DeleteAuthorCommand{ Id = id}));
        }
    }
}
