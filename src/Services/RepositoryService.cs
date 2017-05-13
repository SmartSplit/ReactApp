using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;

namespace Services
{
    public interface IRepositoryService<T> where T : IModel<string>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(string id);
        //IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        //ServiceResult Add(T entity);
        //ServiceResult Delete(T entity);
        //ServiceResult Edit(T entity);
        //ServiceResult Save();

    }

    public class RepositoryService<T> : IRepositoryService<T> where T : class, IModel<string>
    {
        protected IConsumer _consumer;
        protected string _resourcePath;

        public RepositoryService(IConsumer consumer)
        {
            _consumer = consumer;
            //todo maybe better?
            var sampleInstance = (T)Activator.CreateInstance(typeof(T), new object[] {  });
            _resourcePath = sampleInstance.ResourcePath;
        }

        //public virtual ServiceResult Add(T entity)
        //{
        //    ServiceResult result = new ServiceResult();
        //    try
        //    {
        //        _set.Add(entity);
        //        result = Save();
        //    }
        //    catch (Exception e)
        //    {
        //        result.Result = ServiceResultStatus.Error;
        //        result.Messages.Add(e.Message);
        //    }

        //    return result;
        //}

        //public virtual ServiceResult Delete(T entity)
        //{
        //    ServiceResult result = new ServiceResult();
        //    try
        //    {
        //        _set.Remove(entity);
        //        result = Save();
        //    }
        //    catch (Exception e)
        //    {
        //        result.Result = ServiceResultStatus.Error;
        //        result.Messages.Add(e.Message);
        //    }
        //    return result;
        //}

        //public virtual ServiceResult Edit(T entity)
        //{
        //    ServiceResult result = new ServiceResult();
        //    try
        //    {
        //        //(_context as DbContext).Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //        result = Save();
        //    }
        //    catch (Exception e)
        //    {
        //        result.Result = ServiceResultStatus.Error;
        //        result.Messages.Add(e.Message);
        //    }
        //    return result;
        //}

        //public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        //{
        //    IQueryable<T> query = _set.Where(predicate);
        //    return query;
        //}

        public async virtual Task<List<T>> GetAll()
        {
            var responseObject = await _consumer.MakeCall(_resourcePath);
            var users = JsonConvert.DeserializeObject<List<T>>((responseObject.data.ToString()));

            return users;
        }

        public async virtual Task<T> GetById(string id)
        {
            var responseObject = await _consumer.MakeCall(_resourcePath + "?filters=id=" + id);
            var users = JsonConvert.DeserializeObject<List<T>>((responseObject.data.ToString()));

            return users.FirstOrDefault();
        }

        //public virtual ServiceResult Save()
        //{
        //    ServiceResult result = new ServiceResult();
        //    try
        //    {
        //        ((DbContext)_context).SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        result.Result = ServiceResultStatus.Error;
        //        result.Messages.Add(e.Message);
        //    }

        //    return result;

        //}
    }
}
