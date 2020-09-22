using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("Player")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;

    private readonly IRepository _iRepository;

    public PlayerController(ILogger<PlayerController> logger, IRepository iRepository)
    {
        _logger = logger;
        _iRepository = iRepository;
    }

    [HttpGet]
    [Route("Get/{id:Guid}")]
    public async Task<Player> Get(Guid id)
    {
        return await _iRepository.Get(id);
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<Player[]> GetAll()
    {
        return await _iRepository.GetAll();
    }

    [HttpPost]
    [Route("Create")]
    public async Task<Player> Create(NewPlayer player)
    {
        Player newPlayer = new Player() { Name = player.Name, Id = Guid.NewGuid(), Score = 0, Level = 0, IsBanned = false, CreationTime = DateTime.UtcNow };
        await _iRepository.Create(newPlayer);

        return newPlayer;
    }

    [HttpPost]
    [Route("Modify/{id:Guid}")]
    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        return await _iRepository.Modify(id, player);
    }

    [HttpDelete]
    [Route("Delete/{id:Guid}")]
    public async Task<Player> Delete(Guid id)
    {
        return await _iRepository.Delete(id);
    }
}