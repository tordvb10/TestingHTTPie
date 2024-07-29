using TestingHTTPie.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingHTTPie.Interfaces
{
    public interface IPersonRepository
    {

        Task<bool> CreatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(Person person);
        Task<Person> GetPersonAsync(Guid id);
        Task<ICollection<Person>> GetPersonsAsync();
        Task<bool> PersonExistsAsync(Guid id);
        Task<bool> SaveAsync();
        Task<bool> UpdatePersonAsync(Person person);

    }
}
