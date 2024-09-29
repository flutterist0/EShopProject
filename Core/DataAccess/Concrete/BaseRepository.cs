using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete
{
    public class BaseRepository<TEntiy, TContext>(TContext context) : IBaseRepository<TEntiy>
        where TEntiy : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private readonly TContext _context = context;

        //public void Add(TEntiy entity)
        //{ 
        //        var addedEntity = _context.Entry(entity);
        //        addedEntity.State = EntityState.Added;
        //        _context.SaveChanges();
        //}

        public void Add(TEntiy entity)
        {
            var entityType = typeof(TEntiy);
            var idProperty = entityType.GetProperty("Id");

            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(entity);

                if (idValue != null && !idValue.Equals(default(int)))
                {
                    var existingEntity = _context.Set<TEntiy>().Find(idValue);

                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).State = EntityState.Detached;
                    }
                }
            }

            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Delete(TEntiy entity)
        {
            var entityType = typeof(TEntiy);
            var idProperty = entityType.GetProperty("Id");
            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(entity);
                var model = _context.Set<TEntiy>().Find(idValue);
                if (model != null)
                {
                    _context.Entry(model).State = EntityState.Detached;
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
     

        }

        //public void DeleteRange(IEnumerable<TEntiy> entities)
        //{
        //    _context.RemoveRange(entities);
        //    _context.SaveChanges();

        //}

        public void DeleteRange(IEnumerable<TEntiy> entities)
        {
            var entityType = typeof(TEntiy);
            foreach (var entity in entities)
            {
                var idProperty = entityType.GetProperty("Id");
                if (idProperty != null)
                {
                    var idValue = idProperty.GetValue(entity);
                    var model = _context.Set<TEntiy>().Find(idValue);

                    if (model != null)
                    {
                        _context.Entry(model).State = EntityState.Deleted;
                    }
                }
            }
            _context.SaveChanges();
        }


        public void DeleteX(TEntiy entity)
        {
            var entityType = typeof(TEntiy);
            var idProperty = entityType.GetProperty("Id");
            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(entity);
                var model = _context.Set<TEntiy>().Find(idValue);
                if (model != null)
                {
                    _context.Entry(model).State = EntityState.Detached;
                    _context.Set<TEntiy>().Remove(entity);
                    _context.SaveChanges();
                }
            }
        
        }

        public TEntiy Get(Expression<Func<TEntiy, bool>> filter)
        {
            return _context.Set<TEntiy>().SingleOrDefault(filter);
        }

        public List<TEntiy> GetAll(Expression<Func<TEntiy, bool>> filter = null)
        {
            return filter !=null? _context.Set<TEntiy>().Where(filter).ToList() : _context.Set<TEntiy>().ToList();
        }

		public int SaveChanges()
		    => _context.SaveChanges();

		public void Update(TEntiy entity)
        {
            var entityType = typeof(TEntiy);
            var idProperty = entityType.GetProperty("Id");
            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(entity);
                var model = _context.Set<TEntiy>().Find(idValue);
                if (model != null)
                {
                    _context.Entry(model).State = EntityState.Detached;
                    _context.Entry(entity).State = EntityState.Modified;

                    _context.SaveChanges();
                }
            }

        }
   
    }
}
