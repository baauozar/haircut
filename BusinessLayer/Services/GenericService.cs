using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
       

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
           
        }

        public Task<T> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<T>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(T entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(T entity) => _repository.UpdateAsync(entity);
        public Task<bool> SoftDeleteAsync(int id) => _repository.SoftDeleteAsync(id);
        public Task<bool> RestoreAsync(int id) => _repository.RestoreAsync(id);

    }
}
