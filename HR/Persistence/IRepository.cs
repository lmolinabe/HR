﻿using System.Collections.Generic;

namespace HR.Persistence
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
