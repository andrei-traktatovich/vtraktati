using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.DataAccess;
using VTraktate.Domain.Interfaces;
using System.Data.Entity;
using VTraktate.Domain;
using System.Data.Entity.Validation;

namespace VTraktate.Repository
{
    public abstract class Repo<T> : IRepo<T> 
        where T : class, IEntity
    {
        public TraktatContext Context;
        public Repo(TraktatContext ctx)
        {
            Context = ctx;
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return GetAny<T>(predicate);
        }

        public IQueryable<TResult> GetAny<TResult>(Expression<Func<TResult, bool>> predicate = null)
            where TResult : class, IEntity
        {
            IQueryable<TResult> query = Context.Set<TResult>();

            if (query is IQueryable<ITimeStamped>)
                query = query.Include("CreatedBy").Include("ModifiedBy");

            if (predicate != null)
                return query.Where(predicate);
            else
                return query;
        }


        public Task<T> FindByIdAsync(int id)
        {
            return Context.Set<T>().FindAsync(id);
        }


        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }


        public virtual void AddOrUpdate(T entity)
        {
            //TODO: do something if entity already exists !!! 
            // this is the scenario of adding stuff ... 
            // think about it...
            var existingItem = Context.Set<T>().Find(entity.Id);
            if (existingItem == null)
            {
                Context.Set<T>().Add(entity);
                Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                ProcessChanges(existingItem, entity);
                Context.Entry(existingItem).State = EntityState.Detached;
                Context.Set<T>().Add(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        protected virtual void ProcessChanges(T existingItem, T newItem)
        {
            // nop
        }


        public virtual async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            DeleteItem(item);
        }

        public void DeleteItem(T item)
        {
            Context.Set<T>().Remove(item);
        }
        protected async Task<T> GetByIdAsync(int id)
        {
            var item = await Context.Set<T>().FindAsync(id);
            if (item == null)
                throw new InvalidOperationException("Удаление невозможно, т.к. объект не найден.");
            return item;
        }

        public async Task<int> SaveAsUserAsync(int userId)
        {
            return await Context.SaveChangesAsync(userId);
        }

        public async Task<DbResult> TrySaveAsUserAsync(int userId)
        {
            try
            {
                await SaveAsUserAsync(userId);
                return DbResult.Ok();
            }
            catch(DbEntityValidationException ex)
            {
                string[] errorMessages = GetValidationErrors(ex).ToArray();
                return DbResult.Error(errorMessages);
            }
        }

        private IEnumerable<string> GetValidationErrors(DbEntityValidationException ex)
        {
            return ex
                .EntityValidationErrors
                .SelectMany(x => x.ValidationErrors)
                .Select(x => x.ErrorMessage);
        }

        public abstract IQueryable<T> GetGraphs(Expression<Func<T, bool>> predicate = null);


        public TResult FindAnyById<TResult>(int id) where TResult : class, IEntity
        {
            return Context.Set<TResult>().Find(id);
        }


        public async Task<TResult> FindAnyByIdAsync<TResult>(int id) where TResult : class, IEntity
        {
            return await Context.Set<TResult>().FindAsync(id);
        }
    }
}
