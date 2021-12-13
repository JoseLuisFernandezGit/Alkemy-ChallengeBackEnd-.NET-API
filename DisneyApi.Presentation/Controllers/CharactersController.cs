using AutoMapper;
using DisneyApi.Domain.Dtos;
using DisneyApi.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DisneyApi.Presentation.Controllers
{
    [Route("characters")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CharactersController : ControllerBase
    {
        private readonly ICharactersService _service;
        private readonly IMapper _mapper;

        public CharactersController(ICharactersService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CharacterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCharacters(string name = null, int? age = null, int? idMovie = null, int? weight = null)
        {
            try
            {
                
                if(name != null && age == null && idMovie == null && weight == null)
                {
                    var character = _service.GetCharacterByName(name);
                    var characterMapped = _mapper.Map<CharacterDto>(character);

                    return Ok(characterMapped);
                }

                else if(name == null && age !=null && idMovie == null && weight == null)
                {
                    var characters = _service.GetCharactersByAge(age.Value);
                    var charactersMapped = _mapper.Map<List<CharacterDto>>(characters);

                    return Ok(charactersMapped);
                }

                else if(name == null && age == null && idMovie !=null && weight == null)
                {
                    var characters = _service.GetCharacterByMovieId(idMovie.Value);
                    var charactersMapped = _mapper.Map<List<CharacterDto>>(characters);

                    return Ok(charactersMapped);
                }

                else if(name == null && age == null && idMovie == null && weight != null)
                {
                    var characters = _service.GetCharactersByWeight(weight.Value);
                    var charactersMapped = _mapper.Map<List<CharacterDto>>(characters);

                    return Ok(charactersMapped);
                }

                else
                {
                    var characters = _service.GetAllCharacters();
                    var charactersMapped = _mapper.Map<List<CharacterDto>>(characters);

                    return Ok(charactersMapped);
                }
                
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult CreateCharacter([FromBody] CharacterDtoForCreationOrUpdate personaje)
        {
            try
            {
                var characterEntity = _service.CreatePersonaje(personaje);

                if (characterEntity != null)
                {
                    var personajeCreado = _mapper.Map<CharacterDto>(characterEntity);
                    return Created("~characters/", personajeCreado);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCharacter(int id)
        {
            try
            {
                var character = _service.GetCharacterById(id);

                if (character == null)
                {
                    return NotFound();
                }

                _service.Delete(character);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCharacter(int id, [FromBody] CharacterDtoForCreationOrUpdate character)
        {
            try
            {
                if (character == null)
                {
                    return BadRequest("Todos los campos deben estar completos para poder realizar la actualización de este elemento.");
                }

                var characterEntity = _service.GetCharacterById(id);

                if (characterEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(character, characterEntity);
                _service.Update(characterEntity);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("{id}/details")]
        [ProducesResponseType(typeof(CharacterDtoForDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCharactersWithDetails(int id)
        {
            try
            {
                var character = _service.GetCharacteWithDetails(id);

                return Ok(character);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
