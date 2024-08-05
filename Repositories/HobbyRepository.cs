using AutoMapper;
using TestingHTTPie.Data;
using TestingHTTPie.Interfaces;
using TestingHTTPie.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace TestingHTTPie.Repositories
{
    public class HobbyRepository : IHobbyRepository
    {
        private readonly ContextTestingHTTPie _contextTestingHTTPie;
        private readonly IMapper _mapper;
        
        public HobbyRepository(ContextTestingHTTPie contextTestingHTTPie, IMapper mapper)
        {
            _contextTestingHTTPie = contextTestingHTTPie;
            _mapper = mapper;
        }
        public async Task<bool> CreateHobbyAsync(Hobby hobby)
        {
            try
            {
                await _contextTestingHTTPie.Hobbies.AddAsync(hobby);
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

        public async Task<bool> DeleteHobbyAsync(Hobby hobby)
        {
            _contextTestingHTTPie.Hobbies.Remove(hobby);
            return await SaveAsync();
        }

        public async Task<ICollection<Hobby>> GetHobbiesAsync()
        {
            return await _contextTestingHTTPie.Hobbies.ToListAsync();
        }

        public async Task<Hobby> GetHobbyAsync(Guid id)
        {
            return await _contextTestingHTTPie.Hobbies
                .FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new InvalidOperationException($"Entity with id '{id}' not found.");
        }

        public async Task<bool> HobbyExistsAsync(Guid id)
        {
            return await _contextTestingHTTPie.Hobbies.AnyAsync(h => h.Id == id);
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

        public async Task<bool> UpdateHobbyAsync(Hobby hobby)
        {
            try
            {
                // Attach the entity if it's not already being tracked
                var existingHobby = await _contextTestingHTTPie.Hobbies
                    .AsNoTracking()
                    .FirstOrDefaultAsync(h => h.Id == hobby.Id);

                if (existingHobby == null)
                {
                    // Handle the case where the entity to update does not exist
                    return false;
                }

                // Mark the entity as modified
                _contextTestingHTTPie.Hobbies.Entry(hobby).State = EntityState.Modified;
                return await SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflict
                await _contextTestingHTTPie.Hobbies.Entry(hobby).ReloadAsync(); // Reload entity from database
                throw; // Re-throw to let the caller handle it
            }
        }

        public async Task<bool> HobbyPersonExistsAsync(Guid hobbyId, Guid personId)
        {
            return await _contextTestingHTTPie.HobbyPersons
                .AnyAsync(hp => hp.HobbyId == hobbyId && hp.PersonId == personId);
        }

        public async Task<bool> RemoveRelHobbyPersonAsync(Guid hobbyId, Guid personId)
        {
            var hobbyperson = await _contextTestingHTTPie.HobbyPersons
                .FirstOrDefaultAsync(hp => hp.HobbyId == hobbyId && hp.PersonId == personId);
            if (hobbyperson == null)  return false; 
            _contextTestingHTTPie.HobbyPersons.Remove(hobbyperson);
            return await SaveAsync();
        }

        public async Task<bool> AddRelHobbyPersonAsync(Guid hobbyId, Guid personId)
        {
            if(await HobbyPersonExistsAsync(hobbyId,personId)) return false;
            await _contextTestingHTTPie.HobbyPersons.AddAsync(new HobbyPerson { HobbyId = hobbyId, PersonId = personId });
            return await SaveAsync();
        }
    }
}
