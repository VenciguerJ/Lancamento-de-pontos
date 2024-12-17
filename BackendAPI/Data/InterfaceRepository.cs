namespace BackendAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository <T>{
    public Task<IEnumerable<T?>> GetAll();
    public Task<T?> Get(int id);
    public Task Insert(T entity);
    public Task Update (T entity);
    public Task Delete (int id);
    public Task AddRecord(T entity);
    public Task<IEnumerable<T>> GetAllRecords();
}