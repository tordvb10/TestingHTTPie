using TestingHTTPie.Data;
using TestingHTTPie.Interfaces;
using TestingHTTPie.Models;

namespace TestingHTTPie.Repositories
{
    public class HobbyRepository : IHobbyRepository
    {
        private readonly ContextTestingHTTPie _contextTestingHTTPie;
        
        public HobbyRepository(ContextTestingHTTPie contextTestingHTTPie)
        {
            _contextTestingHTTPie = contextTestingHTTPie;
        }
        public Task<bool> CreateHobbyAsync(Hobby Hobby)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHobbyAsync(Hobby Hobby)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Hobby>> GetHobbiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Hobby> GetHobbyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HobbyExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateHobbyAsync(Hobby Hobby)
        {
            throw new NotImplementedException();
        }
    }
}
