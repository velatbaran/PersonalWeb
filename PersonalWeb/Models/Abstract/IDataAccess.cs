using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PersonalWeb.Models.Abstract
{
    public interface IDataAccess<T>
    {
        List<T> List();

        IQueryable<T> ListIQeryable();

        T FirstData();

        T Find(Expression<Func<T, bool>> where);

        int Save();

        int Insert(T obj);

        int Update(T obj);

        int Delete(T obj);
    }
}