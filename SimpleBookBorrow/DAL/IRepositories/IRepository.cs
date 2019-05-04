using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookBorrow.DAL.IRepositories
{
    interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);


        //Find Entity
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);



        //Add Entity
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);



        //Remove Entity
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void RemoveByID(int id);


        void Update(TEntity entity);
        void Save();

        void TurnOffProxyCreation();

    }
}
