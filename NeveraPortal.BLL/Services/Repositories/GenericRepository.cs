using Microsoft.EntityFrameworkCore;
using NeveraPortal.BLL.Services.Interfaces;
using NeveraPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.BLL.Services.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {


        internal NeveraPortalContext context;
        internal DbSet<T> dbSet;

        public GenericRepository()
        {
            this.context = new NeveraPortalContext();
            this.dbSet = context.Set<T>();
        }


        public bool Any(Expression<Func<T, bool>> expression)
        {
            var hasEntity = dbSet.Where(q => q.IsDeleted == false).Any(expression);
            return hasEntity;
        }

        public T Create(T entity)
        {
            entity.AddDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            entity.IsDeleted = false;
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Delete(int id)
        {
            var entity = dbSet.Find(id);
            if (entity == null)
            {
                return entity;
            }
            entity.IsDeleted = true;
            entity.UpdateDate = DateTime.Now;
            context.SaveChanges();
            return entity;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
             var entity = dbSet.Where(q => q.IsDeleted == false).FirstOrDefault(expression);
            return entity;
        }

        public List<T> GetAll()
        {
            var entities = dbSet.Where(q => q.IsDeleted == false).ToList();
            return entities;
        }

        public IQueryable<T> GetAllWithExternalQuery(Expression<Func<T, bool>> filter)
        {
             var entities = dbSet.Where(q => q.IsDeleted == false).Where(filter);
            return entities;
        }

        public IQueryable<T> GetAllWithQueryable()
        {
            var entities = dbSet.Where(q => q.IsDeleted == false);
            return entities;
        }

        public T GetById(int id)
        {
             var entity = dbSet.Where(q => q.IsDeleted == false).FirstOrDefault(q => q.Id == id);

            return entity;
        }
        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }


    }
}

