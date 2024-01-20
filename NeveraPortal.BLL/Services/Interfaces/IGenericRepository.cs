using NeveraPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.BLL.Services.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        bool Any(Expression<Func<T, bool>> expression);

        T FirstOrDefault(Expression<Func<T, bool>> expression);

        T GetById(int id);

        T Create(T entity);

        List<T> GetAll();

        IQueryable<T> GetAllWithQueryable();

        IQueryable<T> GetAllWithExternalQuery(Expression<Func<T, bool>> filter);

        T Delete(int id);

        void Update(T entity);
    }
}
