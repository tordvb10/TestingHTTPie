using AutoMapper;
using TestingHTTPie.Dto;
using TestingHTTPie.Interfaces;
using TestingHTTPie.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestingHTTPie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : Controller
    {
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public HobbyController(
            IHobbyRepository hobbyRepository, 
            IPersonRepository personRepository, 
            IMapper mapper)
        {
            _hobbyRepository = hobbyRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HobbyDto>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetHobbies()
        {
            var getHobbiesDto = _mapper.Map<ICollection<Hobby>, List<HobbyDto>>(await _hobbyRepository.GetHobbiesAsync());
            return (!ModelState.IsValid) ? BadRequest(ModelState) : Ok(getHobbiesDto);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateHobby([FromBody] HobbyDtoBase createHobbyDto)
        {
            if (createHobbyDto == null | !ModelState.IsValid) return BadRequest(ModelState);
            var createHobbyExists = (await _hobbyRepository.GetHobbiesAsync())
                .Where(h => h.Activity == createHobbyDto.Activity)
                .FirstOrDefault();
            if (createHobbyExists != null)
            {
                ModelState.AddModelError("", "Hobby already exists.");
                return StatusCode(422, ModelState);
            }
            var createHobby = _mapper.Map<HobbyDtoBase, Hobby>(createHobbyDto);
            if (!await _hobbyRepository.CreateHobbyAsync(createHobby))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created.");
        }

        [HttpDelete("{hobbyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteHobby(Guid hobbyId)
        {
            if (!await _hobbyRepository.HobbyExistsAsync(hobbyId)) return NotFound("Hobby not found.");
            var deleteHobby = await _hobbyRepository.GetHobbyAsync(hobbyId);
            if (deleteHobby == null) return NotFound("Hobby not found.");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _hobbyRepository.DeleteHobbyAsync(deleteHobby))
            {
                ModelState.AddModelError("", "Something went wrong while deleting Hobby.");
                return BadRequest(ModelState);
            }
            return NoContent();
        }



        [HttpGet("{hobbyId}")]
        [ProducesResponseType(200, Type = typeof(HobbyDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetHobby(Guid hobbyId)
        {
            if (!await _hobbyRepository.HobbyExistsAsync(hobbyId)) return NotFound();
            var getHobbyDto = _mapper.Map<Hobby, HobbyDto>(await _hobbyRepository.GetHobbyAsync(hobbyId));
            return (!ModelState.IsValid) ? BadRequest(ModelState) : Ok(getHobbyDto);
        }

        [HttpPut("{hobbyId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateHobby(Guid hobbyId, [FromBody] HobbyDtoBase updateNEWHobbyDto)
        {
            try
            {
                var updateOLDHobby = await _hobbyRepository.GetHobbyAsync(hobbyId);
                if (updateOLDHobby == null) return NotFound("Hobby not found.");
                _mapper.Map(updateNEWHobbyDto, updateOLDHobby);
                if (!await _hobbyRepository.UpdateHobbyAsync(updateOLDHobby))
                    return StatusCode(500, "Failed to update Hobby.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409, "Concurrency conflict occurred. Please retry the operation.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Hobby: {ex.Message}");
            }
        }


        [HttpGet("Person")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HobbyPersonDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetHobbyPersons()
        {
            var getHobbyPersons = await _hobbyRepository.GetRelHobbyPersonsAsync();
            var getHobbyPersonsDto = _mapper.Map<ICollection<HobbyPerson>,List<HobbyPersonDto>>(getHobbyPersons);
            return (!ModelState.IsValid) ? BadRequest(ModelState) : Ok(getHobbyPersonsDto);
        }


        [HttpDelete("Person/{hobbyId}/{personId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteHobbyPerson(Guid hobbyId, Guid personId)
        {
            if (!await _hobbyRepository.HobbyPersonExistsAsync(hobbyId,personId)) return NotFound("HobbyPerson not found.");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _hobbyRepository.RemoveRelHobbyPersonAsync(hobbyId,personId))
            {
                ModelState.AddModelError("", "Something went wrong while deleting HobbyPerson.");
                return BadRequest(ModelState);
            }
            return NoContent();
        }


        [HttpGet("Person/{hobbyId}/{personId}")]
        [ProducesResponseType(200, Type = typeof(HobbyPersonDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetHobbyPerson(Guid hobbyId, Guid personId)
        {
            if (hobbyId == Guid.Empty | personId == Guid.Empty) return BadRequest(ModelState);
            if (!await _hobbyRepository.HobbyExistsAsync(hobbyId)) return NotFound("Hobby not found.");
            if (!await _personRepository.PersonExistsAsync(personId)) return NotFound("Person not found.");
            if (!await _hobbyRepository.HobbyPersonExistsAsync(hobbyId,personId)) return NotFound("HobbyPerson not found.");
            var getHobbyPerson = await _hobbyRepository.GetRelHobbyPersonAsync(hobbyId, personId);
            var getHobbyPersonDto = _mapper.Map<HobbyPersonDto>(getHobbyPerson);
            return Ok(getHobbyPersonDto);
        }



        [HttpPost("Person/{hobbyId}/{personId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateHobbyPerson(Guid hobbyId, Guid personId)
        {
            if (hobbyId == Guid.Empty | personId == Guid.Empty) return BadRequest(ModelState);
            if (!await _hobbyRepository.HobbyExistsAsync(hobbyId)) return NotFound("Hobby not found.");
            if (!await _personRepository.PersonExistsAsync(personId)) return NotFound("Person not found.");
            if (await _hobbyRepository.HobbyPersonExistsAsync(hobbyId, personId))
            {
                ModelState.AddModelError("", "HobbyPerson already exists.");
                return StatusCode(422, ModelState);
            }
            if (!await _hobbyRepository.AddRelHobbyPersonAsync(hobbyId,personId))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created.");
        }



    }
}