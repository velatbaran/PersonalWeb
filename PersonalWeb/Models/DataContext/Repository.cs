using PersonalWeb.Models.Abstract;
using PersonalWeb.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PersonalWeb.Models.DataContext
{
    //SinglePattern Oluşumu
    public class Repository<T> : RepositoryBase,IDataAccess<T> where T : class
    {
        private DbSet<T> _dbSet;

        public Repository()
        {
            _dbSet = context.Set<T>();
        }

        public List<T> List()
        {
            return _dbSet.ToList();
        }

        public IQueryable<T> ListIQeryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        public T FirstData()
        {
            return _dbSet.FirstOrDefault();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public int Insert(T obj)
        {
            _dbSet.Add(obj);

             if(obj is MyEntityBase)
            {
                MyEntityBase meb = obj as MyEntityBase;
                meb.CreatedOn = DateTime.Now;
                meb.ModifiedOn = DateTime.Now;
            }

            return Save();
        }

        public int Update(T obj)
        {

            if (obj is MyEntityBase)
            {
                MyEntityBase meb = obj as MyEntityBase;
                meb.ModifiedOn = DateTime.Now;
            }

            return Save();
        }

        public int Delete(T obj)
        {
            _dbSet.Remove(obj);
           return Save();
        }
    }
}