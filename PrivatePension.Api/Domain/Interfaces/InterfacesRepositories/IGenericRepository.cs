using Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfacesRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<Notifies> Add(T entity);
        Task<Notifies> Update(T entity);
        Task<Notifies> Delete(T entity);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
    }
}
