using System.Security.Claims;
using dotnet_rpg.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            this._characterService = characterService;
        }

        //[AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> GetAll()
        {
            return Ok(await _characterService.getAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> GetOne(int id)
        {
            var response = await _characterService.getCharacterById(id);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> PostCharacter(
            CharacterRequestDto newCharacter)
        {
            return Ok(await _characterService.saveCharacter(newCharacter));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> Put(
            int id, CharacterRequestDto updateCharacter)
        {
            var response = await _characterService.UpdateCharacter(id, updateCharacter);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> Delete(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> PostCharacterSkill(
            CharacterSkillRequestDto newCharacterSkill)
        {
            var response = await _characterService.AddCharacterSkill(newCharacterSkill);
            if(response.Data is null) {
                if (response.Message.Equals("Character not found")) return NotFound(response);
                if (response.Message.Equals("Character already has this skill")) return Conflict(response);
                return BadRequest(response);
            }
           
            return Ok(response);
        }
    }
}