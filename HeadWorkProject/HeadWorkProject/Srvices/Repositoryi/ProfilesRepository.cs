using HeadWorkProject.Model;
using HeadWorkProject.Srvices.Repository;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HeadWorkProject.Srvices.Repositoryi
{
    public class ProfilesRepository : IRepositoryProfile
    {
        private Lazy<SQLiteAsyncConnection> _database;
        public int _UserId { get; set; }
        public ProfilesRepository(int userId)
        {
            _UserId = userId;
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.db3");
                var database = new SQLiteAsyncConnection(path);
                database.CreateTableAsync<Profile>();
                return database;
            });
        }
        public ProfilesRepository()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.db3");
                var database = new SQLiteAsyncConnection(path);
                database.CreateTableAsync<Profile>();
                return database;
            });
        }

        public Task<int> DeleteAsync<T>(T entity) where T : IEntityBaseProfile, new()
        {
            return _database.Value.DeleteAsync(entity);
        }
        public Task<List<T>> GetAllAsync<T>() where T : IEntityBaseProfile, new()
        {
            return _database.Value.Table<T>().ToListAsync();
        }

        public Task<int> InsertAsync<T>(T entity) where T : IEntityBaseProfile, new()
        {
            return _database.Value.InsertAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : IEntityBaseProfile, new()
        {
            return _database.Value.UpdateAsync(entity);
        }
    }
}
