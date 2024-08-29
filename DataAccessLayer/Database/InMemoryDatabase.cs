using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Web.Caching;


namespace DataAccessLayer.Database
{
    public class InMemoryDatabase<T> : IDbWrapper<T> where T : DataEntity
    {
        private Dictionary<Tuple<string, string>, DataEntity> DatabaseInstance;
        private static Cache _cache = null;
        public InMemoryDatabase() { DatabaseInstance = new Dictionary<Tuple<string, string>, DataEntity>(); }
       private static Cache cache
        {
            get {
            if (_cache == null)
                    _cache = (System.Web.HttpContext.Current == null)? System.Web.HttpRuntime.Cache : System.Web.HttpContext.Current.Cache;
                return _cache;
            }
            set {
                _cache = value;
            }
        }
        public static Dictionary<Tuple<string, string>, DataEntity> Get(string key)
        {
            return (Dictionary<Tuple<string, string>, DataEntity>)cache.Get(key);
        }
        public static void Add(string key, Dictionary<Tuple<string, string>, DataEntity> value)
        {
          //  CacheItemPriority priority = CacheItemPriority.NotRemovable;
          //  var expiration = TimeSpan.FromMinutes(5);
            cache.Insert(key, value);
            //cache.Insert(key, value, null, DateTime.MaxValue, expiration, priority, null);
        }
        public static void Remove(string key)
        {
            cache.Remove(key);
        }
       
        public bool Insert(T data)
        {
            try
            {
                DatabaseInstance.Add(Tuple.Create(data.SiteId, data.CompanyCode), data);
                Add("key1", DatabaseInstance);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool Update(T data)
        {
            try
            {
                DatabaseInstance = Get("key1");
                if (DatabaseInstance.ContainsKey(Tuple.Create(data.SiteId, data.CompanyCode)))
                {
                    DatabaseInstance.Remove(Tuple.Create(data.SiteId, data.CompanyCode));
                    Insert(data);
                    
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entities = FindAll();
                return entities.Where(expression.Compile());
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }

        public IEnumerable<T> FindAll()
        {
            try
            {
               DatabaseInstance = Get("key1");
                if (DatabaseInstance == null)
                {
                    DatabaseInstance = new Dictionary<Tuple<string, string>, DataEntity>();
                }
                return DatabaseInstance.Values.OfType<T>();
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }

        //public bool Delete(Expression<Func<T, bool>> expression)
        //{
        //    try
        //    {
        //        var entities = FindAll();
        //        var entity = entities.Where(expression.Compile());
        //        foreach (var dataEntity in entity)
        //        {
        //            DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
        //        }
        //        Add("key1", DatabaseInstance);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public bool Delete(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entities = FindAll();
             
                var entity = entities.Where(expression.Compile()).ToList();
                if (expression.Parameters.Count > 0) {
                    for (int i= 0; i < entity.Count(); i++){
                        
                        DatabaseInstance.Remove(Tuple.Create(entity[i].SiteId, entity[i].CompanyCode));
                    }
                    //foreach (var dataEntity in entity)
                    //{
                    //    DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
                    //}
                    Add("key1", DatabaseInstance);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteAll()
        {
            try
            {
                DatabaseInstance.Clear();
                Remove("key1");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAll(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
        {
            try
            {
                var entities = FindAll();
                var entity = entities.Where(filter.Compile());
                foreach (var dataEntity in entity)
                {
                    var newEntity = UpdateProperty(dataEntity, fieldToUpdate, newValue);

                    DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
                    Insert(newEntity);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private T UpdateProperty(T dataEntity, string key, object value)
        {
            Type t = typeof(T);
            var prop = t.GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
            {
                throw new Exception("Property not found");
            }

            prop.SetValue(dataEntity, value, null);
            return dataEntity;
        }

        public Task<bool> InsertAsync(T data)
        {
            return Task.FromResult(Insert(data));
        }

        public Task<bool> UpdateAsync(T data)
        {
            return Task.FromResult(Update(data));
        }

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(Find(expression));
        }

        public Task<IEnumerable<T>> FindAllAsync()
        {
            return Task.FromResult(FindAll());
        }

        public Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(Delete(expression));
        }

        public Task<bool> DeleteAllAsync()
        {
            return Task.FromResult(DeleteAll());
        }

        public Task<bool> UpdateAllAsync(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
        {
            return Task.FromResult(UpdateAll(filter, fieldToUpdate, newValue));
        }


    }
}
