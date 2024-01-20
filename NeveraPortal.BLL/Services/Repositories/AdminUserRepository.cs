using NeveraPortal.DAL.Models;
using NeveraPortal.BLL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NeveraPortal.BLL.Services.Repositories
{
    public class AdminUserRepository : IGenericRepository<AdminUser>
    {
        internal NeveraPortalContext context;
        internal DbSet<AdminUser> dbSet;

        public AdminUserRepository(NeveraPortalContext context)
        {
            this.context = context;
            this.dbSet = context.Set<AdminUser>();
        }

        public bool Any(Expression<Func<AdminUser, bool>> expression)
        {
            return dbSet.Where(q => !q.IsDeleted).Any(expression);
        }

        public AdminUser Create(AdminUser entity)
        {
            entity.AddDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            entity.IsDeleted = false;
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public AdminUser Delete(int id)
        {
            var entity = dbSet.Find(id);
            if (entity == null)
            {
                return null;
            }
            entity.IsDeleted = true;
            entity.UpdateDate = DateTime.Now;
            context.SaveChanges();
            return entity;
        }

        public AdminUser FirstOrDefault(Expression<Func<AdminUser, bool>> expression)
        {
            return dbSet.Where(q => !q.IsDeleted).FirstOrDefault(expression);
        }

        public IQueryable<AdminUser> GetAll()
        {
            return dbSet.Where(q => !q.IsDeleted);
        }

        public IQueryable<AdminUser> GetAllWithExternalQuery(Expression<Func<AdminUser, bool>> filter)
        {
            return dbSet.Where(q => !q.IsDeleted).Where(filter);
        }

        public IQueryable<AdminUser> GetAllWithQueryable()
        {
            return dbSet.Where(q => !q.IsDeleted);
        }

        public AdminUser GetById(int id)
        {
            return dbSet.Where(q => !q.IsDeleted).FirstOrDefault(q => q.Id == id);
        }

        public void Update(AdminUser entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        List<AdminUser> IGenericRepository<AdminUser>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
