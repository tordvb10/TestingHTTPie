using TestingHTTPie.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingHTTPie.Interfaces
{
    public interface IHobbyRepository
    {
        Task<bool> CreateHobbyAsync(Hobby Hobby);
        Task<bool> DeleteHobbyAsync(Hobby Hobby);
        Task<Hobby> GetHobbyAsync(Guid id);
        Task<ICollection<Hobby>> GetHobbiesAsync();
        Task<bool> HobbyExistsAsync(Guid id);
        Task<bool> SaveAsync();
        Task<bool> UpdateHobbyAsync(Hobby Hobby);

        //Relation to Person
        Task<bool> HobbyPersonExistsAsync(Guid hobbyId, Guid personId);
        Task<bool> RemoveRelHobbyPersonAsync(Guid hobbyId, Guid personId);
        Task<bool> AddRelHobbyPersonAsync(Guid hobbyId, Guid personId);
        Task<HobbyPerson> GetRelHobbyPersonAsync(Guid hobbyId, Guid personId);

    }
}
