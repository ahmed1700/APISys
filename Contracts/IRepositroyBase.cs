using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Contracts
{
    public interface IRepositroyBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges); // read only
        IQueryable<T> FindByCondtion(Expression<Func<T, bool>> expression , bool trackExchanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
