using BEService.Core.CustomEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BEService.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(Guid id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
