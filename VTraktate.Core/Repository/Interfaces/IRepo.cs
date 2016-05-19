using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;


namespace VTraktate.Core.Repository.Interfaces
{
    public interface IRepo<T> 
        where T : class, IEntity
    {
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);

        IQueryable<TResult> GetAny<TResult>(Expression<Func<TResult, bool>> predicate = null)
            where TResult : class, IEntity;

        TResult FindAnyById<TResult>(int id)
            where TResult : class, IEntity;

        Task<TResult> FindAnyByIdAsync<TResult>(int id)
            where TResult : class, IEntity;

        IQueryable<T> GetGraphs(Expression<Func<T, bool>> predicate = null);

        Task<T> FindByIdAsync(int id);

        Task SaveAsync();

        void AddOrUpdate(T entity);

        Task DeleteAsync(int id);

        void DeleteItem(T item);

        Task<int> SaveAsUserAsync(int userId);

        Task<DbResult> TrySaveAsUserAsync(int userId);
    }    

    public class DbResult
    {
        public string ErrorMessage => string.Join("; ", ErrorMessages);
        public IEnumerable<string> ErrorMessages { get; }
        public bool Success { get; }
        private DbResult(bool success, IEnumerable<string> errorMessages = null)
        {
            if (success && errorMessages == null)
                throw new InvalidOperationException();

            Success = success;
            ErrorMessages = errorMessages;
        }

        public static DbResult Ok()
        {
            return new DbResult(true);
        }

        public static DbResult Error(params string[] errorMessages)
        {
            return new DbResult(false, errorMessages);
        }
    }
}
