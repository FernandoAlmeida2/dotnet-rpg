using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            this._characterService = characterService;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Character>> GetAll()
        {
            return Ok(_characterService.getAllCharacters());
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetOne(int id)
        {
            return Ok(_characterService.getCharacterById(id));   
        }

        [HttpPost]
        public ActionResult<List<Character>> Post(Character newCharacter) {
            return Ok(_characterService.saveCharacter(newCharacter));
        }
    }
}