using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClient() => Ok(await _clientService.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> AddClient(Client client)
        {
            await _clientService.AddAsync(client);
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }
    }
}