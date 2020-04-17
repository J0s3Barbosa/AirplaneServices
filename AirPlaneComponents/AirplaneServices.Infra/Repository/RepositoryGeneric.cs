using AirplaneServices.Domain.Interfaces;
using AirplaneServices.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirplaneServices.Infra.Repository
{
    public class RepositoryGeneric<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptionsBuilder<ContextBase> _OptionsBuilder;

        public RepositoryGeneric()
        {
            this._OptionsBuilder = new DbContextOptionsBuilder<ContextBase>();
        }

        public int Add(T Entity)
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                database.Set<T>().Add(Entity);
                return database.SaveChanges();
            }
        }

        public int Delete(T Entity)
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                database.Set<T>().Remove(Entity);
                return database.SaveChanges();
            }
        }

        public T GetEntity(Guid id)
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                return database.Set<T>().Find(id);
            }
        }

        public List<T> List()
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                return database.Set<T>().ToList();
            }
        }

        public int Update(T Entity)
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                database.Set<T>().Update(Entity);
                return database.SaveChanges();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public void Dispose(bool isDispose)
        {
            if (!isDispose) return;
            GC.SuppressFinalize(this);
        }

        ~RepositoryGeneric()
        {
            this.Dispose(false);
        }
    }

}