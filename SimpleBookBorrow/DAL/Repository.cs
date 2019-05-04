using SimpleBookBorrow.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBookBorrow.Book_DbContext;
using System.Linq.Expressions;
using System.Data.Entity;
using SimpleBookBorrow.Models;


namespace SimpleBookBorrow.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private BorrowBook_DbContext _context;
        public Repository()
        {
            _context = new BorrowBook_DbContext();
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            Save();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            Save();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            Save();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            Save();
        }

        public void RemoveByID(int id)
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(entity);
            Save();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void TurnOffProxyCreation()
        {
            _context.Configuration.ProxyCreationEnabled = false;
        }

    }
}