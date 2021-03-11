using HeadWorkProject.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeadWorkProject.Srvices.Repository
{
    public interface IRepositoryProfile
    {
        Task<int> InsertAsync<T>(T entity) where T : IEntityBaseProfile, new();
        Task<int> DeleteAsync<T>(T entity) where T : IEntityBaseProfile, new();
        Task<int> UpdateAsync<T>(T entity) where T : IEntityBaseProfile, new();
        Task <List<T>> GetAllAsync<T>() where T : IEntityBaseProfile, new();

        Task<int> DeleteAllAsync<T>() where T : IEntityBaseProfile, new();
    }
}
