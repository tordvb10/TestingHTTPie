using AutoMapper;
using TestingHTTPie.Data;
using TestingHTTPie.Interfaces;
using TestingHTTPie.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace TestingHTTPie.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ContextTestingHTTPie _contextTestingHTTPie;
        private readonly IMapper _mapper;

        public PersonRepository(ContextTestingHTTPie contextTestingHTTPie, IMapper mapper)
        {
            _contextTestingHTTPie = contextTestingHTTPie;
            _mapper = mapper;
        }
        public async Task<bool> CreatePersonAsync(Person person)
        {
            try
            {
                await _contextTestingHTTPie.Persons.AddAsync(person);
                return await SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                // Check for specific SQL exception (515) related to column not allowing nulls
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 515)
                {
                    throw new InvalidOperationException("Cannot insert null into RowVersion column.", ex);
                }

                // Handle other database update exceptions
                throw new Exception("Error saving changes.", ex);
            }
        }

        public async Task<bool> DeletePersonAsync(Person person)
        {

            var hobbypersons = await _contextTestingHTTPie.HobbyPersons
                .Where(hp => hp.PersonId == person.Id)
                .ToListAsync();
            _contextTestingHTTPie.HobbyPersons.RemoveRange(hobbypersons);
            _contextTestingHTTPie.Persons.Remove(person);
            return await SaveAsync();
        }

        public async Task<ICollection<Person>> GetPersonsAsync()
        {
            return await _contextTestingHTTPie.Persons
                .Include(h => h.HobbyPersons).ThenInclude(hp => hp.Hobby)
                .ToListAsync();
        }

        public async Task<Person> GetPersonAsync(Guid id)
        {
            return await _contextTestingHTTPie.Persons
                .Include(h => h.HobbyPersons).ThenInclude(hp => hp.Hobby)
                .FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new InvalidOperationException($"Entity with id '{id}' not found.");
        }

        public async Task<bool> PersonExistsAsync(Guid id)
        {
            return await _contextTestingHTTPie.Persons.AnyAsync(h => h.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _contextTestingHTTPie.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log or handle the concurrency exception
                throw new Exception("Concurrency error occurred while saving changes.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Log or handle other database update errors
                throw new Exception("Error saving changes.", ex);
            }
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            try
            {
                // Attach the entity if it's not already being tracked
                var existingPerson = await _contextTestingHTTPie.Persons
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == person.Id);

                if (existingPerson == null)
                {
                    // Handle the case where the entity to update does not exist
                    return false;
                }

                // Mark the entity as modified
                _contextTestingHTTPie.Persons.Entry(person).State = EntityState.Modified;
                return await SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflict
                await _contextTestingHTTPie.Persons.Entry(person).ReloadAsync(); // Reload entity from database
                throw; // Re-throw to let the caller handle it
            }
        }
    }
}
