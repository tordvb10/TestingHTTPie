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
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PersonDto>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPersons()
        {
            var getPersonsDto = _mapper.Map<ICollection<Person>, List<PersonDto>>(await _personRepository.GetPersonsAsync());
            return (!ModelState.IsValid) ? BadRequest(ModelState) : Ok(getPersonsDto);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto createPersonDto)
        {
            if (createPersonDto == null | !ModelState.IsValid) return BadRequest(ModelState);
            var createPersonExists = (await _personRepository.GetPersonsAsync())
                .Where(p => p.FirstName == createPersonDto.FirstName && p.LastName == createPersonDto.LastName)
                .FirstOrDefault();
            if (createPersonExists != null)
            {
                ModelState.AddModelError("", "Employee already exists.");
                return StatusCode(422, ModelState);
            }
            var createPerson = _mapper.Map<PersonDto, Person>(createPersonDto);
            if (!await _personRepository.CreatePersonAsync(createPerson))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created.");
        }

        [HttpDelete("{personId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePerson(Guid personId)
        {
            if (!await _personRepository.PersonExistsAsync(personId)) return NotFound("Person not found.");
            var deletePerson = await _personRepository.GetPersonAsync(personId);
            if (deletePerson == null) return NotFound("Person not found.");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _personRepository.DeletePersonAsync(deletePerson))
            {
                ModelState.AddModelError("", "Something went wrong while deleting Employee.");
                return BadRequest(ModelState);
            }
            return NoContent();
        }



        [HttpGet("{personId}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPerson(Guid personId)
        {
            if (!await _personRepository.PersonExistsAsync(personId)) return NotFound();
            var getPersonDto = _mapper.Map<Person, PersonDto>(await _personRepository.GetPersonAsync(personId));
            return (!ModelState.IsValid) ? BadRequest(ModelState) : Ok(getPersonDto);
        }

        [HttpPut("{personId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePerson(Guid personId, [FromBody] PersonDto updateNEWPersonDto)
        {
            try
            {
                var updateOLDPerson = await _personRepository.GetPersonAsync(personId);
                if (updateOLDPerson == null) return NotFound("Person not found.");
                _mapper.Map(updateNEWPersonDto, updateOLDPerson);
                if (!await _personRepository.UpdatePersonAsync(updateOLDPerson))
                    return StatusCode(500, "Failed to update Person.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409, "Concurrency conflict occurred. Please retry the operation.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Employee: {ex.Message}");
            }
        }

    }
}